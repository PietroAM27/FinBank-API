namespace FinBank.Api.Models;
public class LoanApplication
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int TermMonths { get; set; }
    public decimal InterestRate { get; set; }
    public string Status { get; set; } = "Pending";
    public int UserId { get; set; }
    public User? User { get; set; }
}
