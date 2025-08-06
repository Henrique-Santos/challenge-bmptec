namespace Chu.Bank.Inc.Domain.Services.Transactions;

public interface IHolidayService
{
    Task<bool> IsHolidayAsync(DateTime date);
}