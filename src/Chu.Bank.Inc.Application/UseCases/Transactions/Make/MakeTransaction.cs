using Chu.Bank.Inc.Application.UseCases.Transactions.Common;
using Chu.Bank.Inc.Domain.Entities.Transactions;
using Chu.Bank.Inc.Domain.Repositories;
using Chu.Bank.Inc.Domain.Services.Transactions;

namespace Chu.Bank.Inc.Application.UseCases.Transactions.Make;

public class MakeTransaction : IMakeTransaction
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accoutRepository;
    private readonly ITransactionPolicy _transactionPolicy;

    public MakeTransaction(ITransactionRepository transactionRepository, IAccountRepository accoutRepository, ITransactionPolicy transactionPolicy)
    {
        _transactionRepository = transactionRepository;
        _accoutRepository = accoutRepository;
        _transactionPolicy = transactionPolicy;
    }

    public async Task<TransactionOutput> Handle(MakeTransactionInput request, CancellationToken cancellationToken)
    {
        if (request.FromAccountId == request.ToAccountId)
        {
            throw new InvalidOperationException("Cannot transfer to the same account.");
        }

        var fromAccount = await _accoutRepository.GetByIdAsync(request.FromAccountId, cancellationToken);

        if (fromAccount is null)
        {
            throw new InvalidOperationException("From account does not exist.");
        }

        var toAccount = await _accoutRepository.GetByIdAsync(request.ToAccountId, cancellationToken);

        if (toAccount is null)
        {
            throw new InvalidOperationException("To account does not exist.");
        }

        if (!fromAccount.HasSufficientBalance(request.Amount))
        {
            throw new InvalidOperationException("Insufficient balance in the from account.");
        }

        var canProcessTransaction = await _transactionPolicy.CanProcessTransactionAsync(DateTime.UtcNow);

        if (!canProcessTransaction)
        {
            throw new InvalidOperationException("Transactions cannot be processed on weekends or holidays.");
        }

        fromAccount.Withdraw(request.Amount);

        toAccount.Deposit(request.Amount);

        await _accoutRepository.UpdateAsync(fromAccount, cancellationToken);

        await _accoutRepository.UpdateAsync(toAccount, cancellationToken);

        var transaction = new Transaction(fromAccount.Id, toAccount.Id, request.Amount);

        await _transactionRepository.CreateAsync(transaction, cancellationToken);

        return new TransactionOutput(
            transaction.Id,
            transaction.AccountId,
            transaction.ToAccountId,
            transaction.Amount,
            transaction.Date
        );
    }
}