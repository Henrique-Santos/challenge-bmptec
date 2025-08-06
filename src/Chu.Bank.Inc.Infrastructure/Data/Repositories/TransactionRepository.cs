using Chu.Bank.Inc.Domain.Entities.Transactions;
using Chu.Bank.Inc.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chu.Bank.Inc.Infrastructure.Data.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        await _context.Transactions.AddAsync(transaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime? endDate, CancellationToken cancellationToken)
    {
        var transactions = await _context.Transactions
            .Where(t => t.Date >= startDate && (endDate == null || t.Date <= endDate))
            .ToListAsync(cancellationToken);

        return transactions;
    }
}