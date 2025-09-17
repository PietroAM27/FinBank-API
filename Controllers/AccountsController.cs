using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinBank.Api.Data;
namespace FinBank.Api.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AccountsController:ControllerBase
{
    private readonly FinBankContext _db;
    public AccountsController(FinBankContext db){ _db=db; }
    [HttpGet("me")]
    public async Task<IActionResult> GetMyAccount()
    {
        var username = User.Identity?.Name;
        var acc = await _db.Accounts.Include(a=>a.Transactions).Include(a=>a.User).Where(a=>a.User!.Username==username).FirstOrDefaultAsync();
        if(acc==null) return NotFound();
        return Ok(new { iban=acc.Iban, balance=acc.Balance, transactions=acc.Transactions.OrderByDescending(t=>t.Timestamp).Select(t=> new {t.Id,t.Timestamp,t.Amount,t.Description}) });
    }
}
