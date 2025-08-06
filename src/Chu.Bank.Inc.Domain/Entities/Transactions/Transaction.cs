namespace Chu.Bank.Inc.Domain.Entities.Transactions;

public class Transactions
{
    public Guid Id { get; private set; }
    public Guid FromAccountId { get; private set; }
    public Guid ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    public Transactions(Guid fromAccountId, Guid toAccountId, decimal amount)
    {
        Id = Guid.NewGuid();
        Date = DateTime.UtcNow;
        FromAccountId = fromAccountId;
        ToAccountId = toAccountId;
        Amount = amount;
    }
}