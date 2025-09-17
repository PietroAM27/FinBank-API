using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace FinBank.Api.Services;
public class JwtOptions { public string Key {get;set;}=default!; public string Issuer{get;set;}=default!; public string Audience{get;set;}=default!; public int ExpiresMinutes{get;set;}=120; }
public interface IJwtService { string CreateToken(string username,string role); }
public class JwtService : IJwtService
{
    private readonly JwtOptions _opts;
    public JwtService(IOptions<JwtOptions> opts){ _opts = opts.Value; }
    public string CreateToken(string username,string role)
    {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role ?? "User")
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opts.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_opts.Issuer,_opts.Audience,claims,expires:DateTime.UtcNow.AddMinutes(_opts.ExpiresMinutes),signingCredentials:creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
