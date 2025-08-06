using Chu.Bank.Inc.Application.UseCases.Accounts.Common;
using MediatR;

namespace Chu.Bank.Inc.Application.UseCases.Accounts.Create;

public interface ICreateAccount : IRequestHandler<CreateAccountInput, AccountOutput>
{
}