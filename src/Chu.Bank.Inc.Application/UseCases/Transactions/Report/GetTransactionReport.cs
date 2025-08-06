using Chu.Bank.Inc.Application.UseCases.Transactions.Common;
using Chu.Bank.Inc.Domain.Repositories;

namespace Chu.Bank.Inc.Application.UseCases.Transactions.Report;

public class GetTransactionReport : IGetTransactionReport
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionReport(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<List<TransactionOutput>> Handle(GetTransactionReportInput request, CancellationToken cancellationToken)
    {
        var transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(request.StartDate, request.EndDate, cancellationToken);
        
        return transactions.Select(t => new TransactionOutput(t.Id, t.AccountId, t.ToAccountId, t.Amount, t.Date)).ToList();
    }
}