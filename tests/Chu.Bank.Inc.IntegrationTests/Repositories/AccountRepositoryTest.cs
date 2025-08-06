using Chu.Bank.Inc.Domain.Entities.Accounts;
using Chu.Bank.Inc.Infrastructure.Data.Repositories;
using Chu.Bank.Inc.IntegrationTests.Base;
using FluentAssertions;

namespace Chu.Bank.Inc.IntegrationTests.Repositories;

public class AccountRepositoryTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;

    public AccountRepositoryTest(BaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetAccountByUserId_ShouldReturnAccount_WhenExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var account = new Account(userId, 100.0m);
        await _fixture.Context.Accounts.AddAsync(account);
        await _fixture.Context.SaveChangesAsync();

        // Act
        var repository = new AccountRepository(_fixture.Context);
        var result = await repository.GetByUserIdAsync(userId, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(userId);
        result.Balance.Should().Be(100.0m);
    }

    [Fact]
    public async Task GetAccountByUserId_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var repository = new AccountRepository(_fixture.Context);

        // Act
        var result = await repository.GetByUserIdAsync(userId, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAccountById_ShouldReturnAccount_WhenExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var account = new Account(userId, 500.0m);
        await _fixture.Context.Accounts.AddAsync(account);
        await _fixture.Context.SaveChangesAsync();

        // Act
        var repository = new AccountRepository(_fixture.Context);
        var result = await repository.GetByIdAsync(account.Id, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(account.Id);
        result.UserId.Should().Be(userId);
        result.Balance.Should().Be(500.0m);
    }

    [Fact]
    public async Task GetAccountById_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var repository = new AccountRepository(_fixture.Context);

        // Act
        var result = await repository.GetByIdAsync(accountId, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateAccount_ShouldAddAccountToDatabase()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var account = new Account(userId, 200.0m);
        var repository = new AccountRepository(_fixture.Context);

        // Act
        await repository.CreateAsync(account, CancellationToken.None);
        var result = await _fixture.Context.Accounts.FindAsync(account.Id);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(userId);
        result.Balance.Should().Be(200.0m);
    }

    [Fact]
    public async Task UpdateAccount_ShouldModifyExistingAccount()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var account = new Account(userId, 300.0m);
        await _fixture.Context.Accounts.AddAsync(account);
        await _fixture.Context.SaveChangesAsync();

        // Act
        account.Deposit(100.0m);
        var repository = new AccountRepository(_fixture.Context);
        await repository.UpdateAsync(account, CancellationToken.None);
        var updatedAccount = await _fixture.Context.Accounts.FindAsync(account.Id);

        // Assert
        updatedAccount.Should().NotBeNull();
        updatedAccount.Balance.Should().Be(400.0m);
    }
}