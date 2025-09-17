using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinBank.Api.DTOs;
namespace FinBank.Api.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class LoansController:ControllerBase
{
    [HttpPost("simulate")]
    public ActionResult<LoanSimulationResult> Simulate([FromBody] LoanSimulationRequest req)
    {
        double r = req.annualRate/12.0; int n = req.termMonths;
        if(r<=0 || n<=0) return BadRequest("Invalid rate or term.");
        double pv = (double)req.amount;
        double pmt = pv * (r * Math.Pow(1+r,n)) / (Math.Pow(1+r,n)-1);
        decimal monthlyPayment = decimal.Round((decimal)pmt,2);
        decimal totalCost = monthlyPayment*n;
        decimal totalInterest = totalCost - req.amount;
        return Ok(new LoanSimulationResult(monthlyPayment, decimal.Round(totalInterest,2), decimal.Round(totalCost,2)));
    }
}
