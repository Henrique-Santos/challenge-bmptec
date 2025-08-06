namespace Chu.Bank.Inc.Domain.Entities.Transactions;

public class Transaction
{
    public Guid Id { get; private set; }
    public Guid AccountId { get; private set; }
    public Guid ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    public Transaction(Guid accountId, Guid toAccountId, decimal amount)
    {
        Id = Guid.NewGuid();
        Date = DateTime.UtcNow;
        AccountId = accountId;
        ToAccountId = toAccountId;
        Amount = amount;
    }
}