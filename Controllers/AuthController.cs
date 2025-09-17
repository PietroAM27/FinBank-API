using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinBank.Api.Data;
using FinBank.Api.DTOs;
using FinBank.Api.Services;
namespace FinBank.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController:ControllerBase
{
    private readonly FinBankContext _db; private readonly IJwtService _jwt;
    public AuthController(FinBankContext db, IJwtService jwt){ _db=db; _jwt=jwt; }
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest req)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u=>u.Username==req.Username && u.PasswordHash==req.Password);
        if(user==null) return Unauthorized();
        var token = _jwt.CreateToken(user.Username,user.Role);
        return Ok(new LoginResponse(token,user.Username,user.Role));
    }
}
