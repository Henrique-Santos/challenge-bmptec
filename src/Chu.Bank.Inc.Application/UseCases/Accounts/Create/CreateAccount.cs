using Chu.Bank.Inc.Application.UseCases.Accounts.Common;
using Chu.Bank.Inc.Domain.Entities.Accounts;
using Chu.Bank.Inc.Domain.Repositories;

namespace Chu.Bank.Inc.Application.UseCases.Accounts.Create;

public class CreateAccount : ICreateAccount
{
    private readonly IAccountRepository _accoutRepository;

    public CreateAccount(IAccountRepository accoutRepository)
    {
        _accoutRepository = accoutRepository;
    }

    public async Task<AccountOutput> Handle(CreateAccountInput request, CancellationToken cancellationToken)
    {
        var dbAccount = await _accoutRepository.GetByUserIdAsync(request.UserId, cancellationToken);

        if (dbAccount is not null)
        {
            throw new InvalidOperationException("An account for this user already exists.");
        }

        var account = new Account(request.UserId, request.Balance);

        await _accoutRepository.CreateAsync(account!, cancellationToken);

        return new AccountOutput(account.Id, account.UserId, account.Balance);
    }
}