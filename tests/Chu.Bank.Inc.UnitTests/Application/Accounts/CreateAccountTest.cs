using Chu.Bank.Inc.Application.UseCases.Accounts.Create;
using Chu.Bank.Inc.Domain.Entities.Accounts;
using Chu.Bank.Inc.Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace Chu.Bank.Inc.UnitTests.Application.Accounts;

public class CreateAccountTest
{
    private readonly IAccountRepository _accoutRepository;

    public CreateAccountTest()
    {
        _accoutRepository = Substitute.For<IAccountRepository>();
    }

    [Fact]
    public async Task Should_CreateAccount_WhenUserDoesNotHaveAnAccount()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var balance = 100.0m;
        var usecase = new CreateAccount(_accoutRepository);
        var input = new CreateAccountInput(userId, balance);

        _accoutRepository.GetByUserIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Account?>(null));

        // Act
        var result = await usecase.Handle(input, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(userId);
        result.Balance.Should().Be(balance);
        await _accoutRepository.Received(1).CreateAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_ThrowException_WhenUserAlreadyHasAnAccount()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var balance = 100.0m;
        var usecase = new CreateAccount(_accoutRepository);
        var input = new CreateAccountInput(userId, balance);

        _accoutRepository.GetByUserIdAsync(userId, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Account?>(new Account(userId, 50.0m)));

        // Act
        var action = async () => await usecase.Handle(input, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("An account for this user already exists.");
        await _accoutRepository.DidNotReceive().CreateAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>());
    }
}