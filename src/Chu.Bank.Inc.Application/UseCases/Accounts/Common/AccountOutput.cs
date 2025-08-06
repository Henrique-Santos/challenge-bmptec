namespace Chu.Bank.Inc.Application.UseCases.Accounts.Common;

public record AccountOutput(Guid Id, Guid UserId, decimal Balance);