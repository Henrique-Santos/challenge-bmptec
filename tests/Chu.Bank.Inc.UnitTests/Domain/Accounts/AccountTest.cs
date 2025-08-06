using Chu.Bank.Inc.Domain.Entities.Accounts;
using FluentAssertions;

namespace Chu.Bank.Inc.UnitTests.Domain.Accounts;

public class AccountTest
{
    [Fact]
    public void Should_CreateAccount_WithValidParameters()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var account = new Account(userId, 100.0m);

        // Assert
        account.Should().NotBeNull();
        account.Id.Should().NotBeEmpty();
        account.UserId.Should().Be(userId);
        account.Balance.Should().Be(100.0m);
        account.Transactions.Should().BeEmpty();
    }

    [Fact]
    public void Should_AllowDeposit()
    {
        // Arrange
        var account = new Account(Guid.NewGuid(), 100.0m);

        // Act
        account.Deposit(50.0m);

        // Assert
        account.Balance.Should().Be(150.0m);
    }

    [Fact]
    public void Should_AllowWithdraw_WhenSufficientBalance()
    {
        // Arrange
        var account = new Account(Guid.NewGuid(), 100.0m);

        // Act
        account.Withdraw(50.0m);

        // Assert
        account.Balance.Should().Be(50.0m);
    }

    [Fact]
    public void Should_NotAllowWithdraw_WhenInsufficientBalance()
    {
        // Arrange
        var account = new Account(Guid.NewGuid(), 100.0m);

        // Act
        var action = () => account.Withdraw(150.0m);

        // Assert
        action.Should().Throw<InvalidOperationException>().WithMessage("Insufficient balance for withdrawal.");
    }
}