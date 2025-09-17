using Microsoft.EntityFrameworkCore;
using FinBank.Api.Models;
namespace FinBank.Api.Data;
public class FinBankContext : DbContext
{
    public FinBankContext(DbContextOptions<FinBankContext> options):base(options){}
    public DbSet<User> Users => Set<User>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<LoanApplication> Loans => Set<LoanApplication>();
    public DbSet<InvestmentPlan> Investments => Set<InvestmentPlan>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasData(new User{ Id=1, Username="demo", PasswordHash="demo", Role="User"});
        modelBuilder.Entity<Account>().HasData(new Account{ Id=1, Iban="IT60X0542811101000000123456", Balance=5320.75M, UserId=1});
        modelBuilder.Entity<Transaction>().HasData(
            new Transaction{ Id=1, AccountId=1, Amount=-45.90M, Description="Supermercato", Timestamp=DateTime.UtcNow.AddDays(-2)},
            new Transaction{ Id=2, AccountId=1, Amount=-12.50M, Description="Caffè e bar", Timestamp=DateTime.UtcNow.AddDays(-1)},
            new Transaction{ Id=3, AccountId=1, Amount=1500.00M, Description="Stipendio", Timestamp=DateTime.UtcNow.AddDays(-10)}
        );
    }
}
