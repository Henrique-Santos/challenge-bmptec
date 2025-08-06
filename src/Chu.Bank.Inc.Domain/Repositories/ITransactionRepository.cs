using Chu.Bank.Inc.Domain.Entities.Transactions;

namespace Chu.Bank.Inc.Domain.Repositories;

public interface ITransactionRepository
{
    Task CreateAsync(Transaction transaction, CancellationToken cancellationToken);
    Task<List<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime? endDate, CancellationToken cancellationToken);
}