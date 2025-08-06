

using Chu.Bank.Inc.Domain.Entities.Transactions;
using FluentAssertions;

namespace Chu.Bank.Inc.UnitTests.Domain.Transactions;

public class TransactionTest
{
    [Fact]
    public void Should_CreateTransaction_WithValidParameters()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var toAccountId = Guid.NewGuid();
        var amount = 100.0m;

        // Act
        var transaction = new Transaction(accountId, toAccountId, amount);

        // Assert
        transaction.Should().NotBeNull();
        transaction.Id.Should().NotBeEmpty();
        transaction.AccountId.Should().Be(accountId);
        transaction.Amount.Should().Be(amount);
        transaction.Date.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}