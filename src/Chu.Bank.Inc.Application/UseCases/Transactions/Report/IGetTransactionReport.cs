using Chu.Bank.Inc.Application.UseCases.Transactions.Common;
using MediatR;

namespace Chu.Bank.Inc.Application.UseCases.Transactions.Report;

public interface IGetTransactionReport : IRequestHandler<GetTransactionReportInput, List<TransactionOutput>>
{
}