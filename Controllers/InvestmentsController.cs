using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinBank.Api.DTOs;
namespace FinBank.Api.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class InvestmentsController:ControllerBase
{
    [HttpPost("simulate")]
    public ActionResult<InvestmentResult> Simulate([FromBody] InvestmentRequest req)
    {
        int months = req.years*12;
        double monthlyRate = req.expectedAnnualReturn/12.0;
        decimal amount = req.initialAmount;
        for(int i=0;i<months;i++){ amount += req.monthlyContribution; amount = amount * (decimal)(1.0+monthlyRate); }
        var contributed = req.initialAmount + req.monthlyContribution*months;
        var gain = amount - contributed;
        return Ok(new InvestmentResult(decimal.Round(amount,2), decimal.Round(contributed,2), decimal.Round(gain,2)));
    }
}
