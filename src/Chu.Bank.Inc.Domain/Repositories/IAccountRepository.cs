using Chu.Bank.Inc.Domain.Entities.Accounts;

namespace Chu.Bank.Inc.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<Account?> GetByIdAsync(Guid accountId, CancellationToken cancellationToken);
    Task CreateAsync(Account account, CancellationToken cancellationToken);
    Task UpdateAsync(Account account, CancellationToken cancellationToken);
}