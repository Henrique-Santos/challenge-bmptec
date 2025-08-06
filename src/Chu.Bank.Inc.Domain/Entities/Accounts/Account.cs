using System.Transactions;

namespace Chu.Bank.Inc.Domain.Entities.Accounts;

public class Account
{
    public Guid Id { get; private set; }
    public string UserId { get; private set; }
    public decimal Balance { get; private set; }
    public List<Transaction> Transactions { get; private set; } = new();

    public Account(string userId, decimal balance)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Balance = balance;
    }
}