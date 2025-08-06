using Chu.Bank.Inc.Application.UseCases.Transactions.Make;
using Chu.Bank.Inc.Domain.Entities.Accounts;
using Chu.Bank.Inc.Domain.Entities.Transactions;
using Chu.Bank.Inc.Domain.Repositories;
using Chu.Bank.Inc.Domain.Services.Transactions;
using FluentAssertions;
using NSubstitute;

namespace Chu.Bank.Inc.UnitTests.Application.Transactions;

public class MakeTransactionTest
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accoutRepository;
    private readonly ITransactionPolicy _transactionPolicy;

    public MakeTransactionTest()
    {
        _transactionRepository = Substitute.For<ITransactionRepository>();
        _accoutRepository = Substitute.For<IAccountRepository>();
        _transactionPolicy = Substitute.For<ITransactionPolicy>();
    }

    [Fact]
    public async Task Should_MakeTransaction_Successfully()
    {
        // Arrange
        var amount = 100.0m;

        var fromAccount = new Account(Guid.NewGuid(), 200.0m);
        var toAccount = new Account(Guid.NewGuid(), 50.0m);
        var usecase = new MakeTransaction(_transactionRepository, _accoutRepository, _transactionPolicy);
        var input = new MakeTransactionInput(fromAccount.Id, toAccount.Id, amount);

        _accoutRepository.GetByIdAsync(fromAccount.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Account?>(fromAccount));

        _accoutRepository.GetByIdAsync(toAccount.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Account?>(toAccount));

        _transactionPolicy.CanProcessTransactionAsync(Arg.Any<DateTime>())
            .Returns(Task.FromResult(true));

        // Act
        var result = await usecase.Handle(input, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.FromAccountId.Should().Be(fromAccount.Id);
        result.ToAccountId.Should().Be(toAccount.Id);
        result.Amount.Should().Be(amount);
        fromAccount.Balance.Should().Be(100.0m);
        toAccount.Balance.Should().Be(150.0m);
        await _accoutRepository.Received(2).UpdateAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>());
        await _transactionRepository.Received(1).CreateAsync(Arg.Any<Transaction>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_ThrowException_WhenTransactionPolicyPreventsProcessing()
    {
        // Arrange
        var amount = 100.0m;
        var fromAccount = new Account(Guid.NewGuid(), 200.0m);
        var toAccount = new Account(Guid.NewGuid(), 50.0m);
        var usecase = new MakeTransaction(_transactionRepository, _accoutRepository, _transactionPolicy);
        var input = new MakeTransactionInput(fromAccount.Id, toAccount.Id, amount);

        _accoutRepository.GetByIdAsync(fromAccount.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Account?>(fromAccount));

        _accoutRepository.GetByIdAsync(toAccount.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Account?>(toAccount));

        _transactionPolicy.CanProcessTransactionAsync(Arg.Any<DateTime>())
            .Returns(Task.FromResult(false));

        // Act
        var action = async () => await usecase.Handle(input, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Transactions cannot be processed on weekends or holidays.");
    }

    [Fact]
    public async Task Should_ThrowException_WhenFromAccountDoesNotExist()
    {
        // Arrange
        var amount = 100.0m;
        var toAccount = new Account(Guid.NewGuid(), 50.0m);
        var usecase = new MakeTransaction(_transactionRepository, _accoutRepository, _transactionPolicy);
        var input = new MakeTransactionInput(Guid.NewGuid(), toAccount.Id, amount);

        _accoutRepository.GetByIdAsync(input.FromAccountId, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Account?>(null));

        _accoutRepository.GetByIdAsync(toAccount.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Account?>(toAccount));

        // Act
        var action = async () => await usecase.Handle(input, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("From account does not exist.");
    }

    [Fact]
    public async Task Should_ThrowException_WhenToAccountDoesNotExist()
    {
        // Arrange
        var amount = 100.0m;
        var fromAccount = new Account(Guid.NewGuid(), 200.0m);
        var usecase = new MakeTransaction(_transactionRepository, _accoutRepository, _transactionPolicy);
        var input = new MakeTransactionInput(fromAccount.Id, Guid.NewGuid(), amount);

        _accoutRepository.GetByIdAsync(fromAccount.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Account?>(fromAccount));

        _accoutRepository.GetByIdAsync(input.ToAccountId, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Account?>(null));

        // Act
        var action = async () => await usecase.Handle(input, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("To account does not exist.");
    }

    [Fact]
    public async Task Should_ThrowException_WhenInsufficientBalance()
    {
        // Arrange
        var amount = 300.0m;
        var fromAccount = new Account(Guid.NewGuid(), 200.0m);
        var toAccount = new Account(Guid.NewGuid(), 50.0m);
        var usecase = new MakeTransaction(_transactionRepository, _accoutRepository, _transactionPolicy);
        var input = new MakeTransactionInput(fromAccount.Id, toAccount.Id, amount);

        _accoutRepository.GetByIdAsync(fromAccount.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Account?>(fromAccount));

        _accoutRepository.GetByIdAsync(toAccount.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Account?>(toAccount));

        _transactionPolicy.CanProcessTransactionAsync(Arg.Any<DateTime>())
            .Returns(Task.FromResult(true));

        // Act
        var action = async () => await usecase.Handle(input, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Insufficient balance in the from account.");
    }

    [Fact]
    public async Task Should_ThrowException_WhenTransferringToSameAccount()
    {
        // Arrange
        var amount = 100.0m;
        var accountId = Guid.NewGuid();
        var usecase = new MakeTransaction(_transactionRepository, _accoutRepository, _transactionPolicy);
        var input = new MakeTransactionInput(accountId, accountId, amount);

        // Act
        var action = async () => await usecase.Handle(input, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Cannot transfer to the same account.");
    }
}