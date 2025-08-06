
namespace Chu.Bank.Inc.Domain.Services.Transactions;

public class TransactionPolicy : ITransactionPolicy
{
    private readonly IHolidayService _holidayService;

    public TransactionPolicy(IHolidayService holidayService)
    {
        _holidayService = holidayService;
    }

    public async Task<bool> CanProcessTransactionAsync(DateTime transactionDate)
    {
        if (transactionDate.DayOfWeek == DayOfWeek.Saturday || transactionDate.DayOfWeek == DayOfWeek.Sunday)
        {
            return false;
        }

        var isHoliday = await _holidayService.IsHolidayAsync(transactionDate);

        return !isHoliday;
    }
}