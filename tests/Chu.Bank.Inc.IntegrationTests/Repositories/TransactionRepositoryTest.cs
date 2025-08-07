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
}