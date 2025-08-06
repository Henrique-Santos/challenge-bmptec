namespace Chu.Bank.Inc.Domain.Services.Transactions;

public interface ITransactionPolicy
{
    Task<bool> CanProcessTransactionAsync(DateTime transactionDate);
}