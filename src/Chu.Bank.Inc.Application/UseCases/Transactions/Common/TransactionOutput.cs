namespace Chu.Bank.Inc.Application.UseCases.Transactions.Common;

public record TransactionOutput(Guid Id, Guid FromAccountId, Guid ToAccountId, decimal Amount, DateTime Date);