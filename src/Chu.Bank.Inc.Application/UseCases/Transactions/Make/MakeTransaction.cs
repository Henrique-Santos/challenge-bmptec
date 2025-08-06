using Chu.Bank.Inc.Application.UseCases.Transactions.Common;

namespace Chu.Bank.Inc.Application.UseCases.Transactions.Make;

public class MakeTransaction : IMakeTransaction
{
    public Task<TransactionOutput> Handle(MakeTransactionInput request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}