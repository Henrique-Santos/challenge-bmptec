using Chu.Bank.Inc.Domain.Entities.Transactions;

namespace Chu.Bank.Inc.Domain.Entities.Accounts;

public class Account
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public decimal Balance { get; private set; }
    public List<Transaction> Transactions { get; private set; } = new();

    public Account(Guid userId, decimal balance)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Balance = balance;
    }

    // For seeder
    public Account(Guid id, Guid userId, decimal balance)
    {
        Id = id;
        UserId = userId;
        Balance = balance;
    }

    public bool HasSufficientBalance(decimal amount)
    {
        return Balance >= amount;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (!HasSufficientBalance(amount))
        {
            throw new InvalidOperationException("Insufficient balance for withdrawal.");
        }
        Balance -= amount;
    }
}