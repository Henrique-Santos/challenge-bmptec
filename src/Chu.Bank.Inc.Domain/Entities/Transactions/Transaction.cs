namespace Chu.Bank.Inc.Domain.Entities.Transactions;

public class Transaction
{
    public Guid Id { get; private set; }
    public Guid AccountId { get; private set; }
    public Guid ToAccountId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }

    public Transaction(Guid accountId, Guid toAccountId, decimal amount)
    {
        Id = Guid.NewGuid();
        Date = DateTime.UtcNow;
        AccountId = accountId;
        ToAccountId = toAccountId;
        Amount = amount;
    }
}