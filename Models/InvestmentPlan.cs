namespace FinBank.Api.Models;
public class InvestmentPlan
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal InitialAmount { get; set; }
    public decimal MonthlyContribution { get; set; }
    public double ExpectedAnnualReturn { get; set; }
    public int Years { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}
