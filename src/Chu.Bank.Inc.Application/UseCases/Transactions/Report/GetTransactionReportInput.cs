using Chu.Bank.Inc.Application.UseCases.Transactions.Common;
using FluentValidation;
using MediatR;

namespace Chu.Bank.Inc.Application.UseCases.Transactions.Report;

public record GetTransactionReportInput(DateTime StartDate, DateTime? EndDate) : IRequest<List<TransactionOutput>>;

public class GetTransactionReportInputValidator : AbstractValidator<GetTransactionReportInput>
{
    public GetTransactionReportInputValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be greater than start date.")
            .When(x => x.EndDate.HasValue);
    }
}