using Chu.Bank.Inc.Application.UseCases.Accounts.Common;
using FluentValidation;
using MediatR;

namespace Chu.Bank.Inc.Application.UseCases.Accounts.Create;

public record CreateAccountInput(Guid UserId, decimal Balance) : IRequest<AccountOutput>;

public class CreateAccountInputValidator : AbstractValidator<CreateAccountInput>
{
    public CreateAccountInputValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId cannot be empty.")
            .NotNull().WithMessage("UserId cannot be null.");

        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0).WithMessage("Balance must be a non-negative value.");
    }
}