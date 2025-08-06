using Chu.Bank.Inc.Domain.Entities.Accounts;
using Chu.Bank.Inc.Domain.Entities.Transactions;
using Chu.Bank.Inc.Infrastructure.Data.Repositories;
using Chu.Bank.Inc.IntegrationTests.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Chu.Bank.Inc.IntegrationTests.Repositories;

public class TransactionRepositoryTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;

    public TransactionRepositoryTest(BaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CreateAsync_ShouldAddTransaction_WhenCalled()
    {
        // Arrange
        var fromAccount = new Account(Guid.NewGuid(), 1000.0m);
        var toAccount = new Account(Guid.NewGuid(), 500.0m);
        await _fixture.Context.Accounts.AddRangeAsync([fromAccount, toAccount]);
        await _fixture.Context.SaveChangesAsync();

        var transaction = new Transaction(fromAccount.Id, toAccount.Id, 250.0m);
        var repository = new TransactionRepository(_fixture.Context);

        // Act
        await repository.CreateAsync(transaction, CancellationToken.None);

        // Assert
        var transactions = await _fixture.Context.Transactions.ToListAsync();

        transactions.Should().HaveCount(1);
        transactions[0].AccountId.Should().Be(fromAccount.Id);
        transactions[0].ToAccountId.Should().Be(toAccount.Id);
        transactions[0].Amount.Should().Be(250.0m);
        transactions[0].Date.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task GetTransactionsByDateRangeAsync_ShouldReturnTransactions_WhenExists()
    {
        // Arrange
        var account = new Account(Guid.NewGuid(), 1000.0m);
        await _fixture.Context.Accounts.AddAsync(account);
        await _fixture.Context.SaveChangesAsync();

        var transaction1 = new Transaction(account.Id, Guid.NewGuid(), 100.0m);
        var transaction2 = new Transaction(account.Id, Guid.NewGuid(), 200.0m);
        await _fixture.Context.Transactions.AddRangeAsync([transaction1, transaction2]);
        await _fixture.Context.SaveChangesAsync();

        var repository = new TransactionRepository(_fixture.Context);

        // Act
        var result = await repository.GetTransactionsByDateRangeAsync(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(t => t.Amount == 100.0m && t.Date == transaction1.Date);
        result.Should().Contain(t => t.Amount == 200.0m && t.Date == transaction2.Date);
    }

    [Fact]
    public async Task GetTransactionsByDateRangeAsync_ShouldReturnEmpty_WhenNoTransactions()
    {
        // Arrange
        var repository = new TransactionRepository(_fixture.Context);

        // Act
        var result = await repository.GetTransactionsByDateRangeAsync(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
}