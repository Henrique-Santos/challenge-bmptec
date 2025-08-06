using Chu.Bank.Inc.Application.UseCases.Accounts.Common;

namespace Chu.Bank.Inc.Application.UseCases.Accounts.Create;

public class CreateAccount : ICreateAccount
{
    public Task<AccountOutput> Handle(CreateAccountInput request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
