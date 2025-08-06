using Chu.Bank.Inc.Application.UseCases.Transactions.Common;
using MediatR;

namespace Chu.Bank.Inc.Application.UseCases.Transactions.Make;

public interface IMakeTransaction : IRequestHandler<MakeTransactionInput, TransactionOutput>
{
}