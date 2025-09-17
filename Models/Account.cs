namespace FinBank.Api.Models;
public class Account
{
    public int Id { get; set; }
    public string Iban { get; set; } = default!;
    public decimal Balance { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
