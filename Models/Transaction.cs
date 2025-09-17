namespace FinBank.Api.Models;
public class Transaction
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public string Description { get; set; } = default!;
    public int AccountId { get; set; }
    public Account? Account { get; set; }
}
