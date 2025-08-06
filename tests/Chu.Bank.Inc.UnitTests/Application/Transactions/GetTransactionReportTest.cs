using Chu.Bank.Inc.Application.UseCases.Transactions.Common;
using Chu.Bank.Inc.Application.UseCases.Transactions.Report;
using Chu.Bank.Inc.Domain.Entities.Transactions;
using Chu.Bank.Inc.Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace Chu.Bank.Inc.UnitTests.Application.Transactions;

public class GetTransactionReportTest
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionReportTest()
    {
        _transactionRepository = Substitute.For<ITransactionRepository>();
    }

    [Fact]
    public async Task Handle_ShouldReturnTransactionReport_WhenTransactionsExist()
    {
        // Arrange
        var startDate = new DateTime(2025, 1, 1);

        var transactions = new List<Transaction>
        {
            new Transaction(Guid.NewGuid(), Guid.NewGuid(), 100),
            new Transaction(Guid.NewGuid(), Guid.NewGuid(), 200)
        };

        _transactionRepository.GetTransactionsByDateRangeAsync(startDate, null, Arg.Any<CancellationToken>())
            .Returns(transactions);

        var usecase = new GetTransactionReport(_transactionRepository);
        var input = new GetTransactionReportInput(startDate, null);

        // Act
        var result = await usecase.Handle(input, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().AllBeOfType<TransactionOutput>();
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoTransactionsExist()
    {
        // Arrange
        var startDate = new DateTime(2025, 1, 1);
        
        _transactionRepository.GetTransactionsByDateRangeAsync(startDate, null, Arg.Any<CancellationToken>())
            .Returns(new List<Transaction>());

        var usecase = new GetTransactionReport(_transactionRepository);
        var input = new GetTransactionReportInput(startDate, null);

        // Act
        var result = await usecase.Handle(input, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}