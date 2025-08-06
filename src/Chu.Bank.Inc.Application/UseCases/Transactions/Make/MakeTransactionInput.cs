using Chu.Bank.Inc.Application.UseCases.Transactions.Common;
using FluentValidation;
using MediatR;

namespace Chu.Bank.Inc.Application.UseCases.Transactions.Make;

public record MakeTransactionInput(Guid ToAccountId, decimal Amount) : IRequest<TransactionOutput>;

public class MakeTransactionInputValidator : AbstractValidator<MakeTransactionInput>
{
    public MakeTransactionInputValidator()
    {
        RuleFor(x => x.ToAccountId)
            .NotEmpty().WithMessage("ToAccountId cannot be empty.")
            .NotNull().WithMessage("ToAccountId cannot be null.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");
    }
}