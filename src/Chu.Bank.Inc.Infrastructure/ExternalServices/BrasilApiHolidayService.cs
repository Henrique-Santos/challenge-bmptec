using System.Net.Http.Json;
using Chu.Bank.Inc.Domain.Services.Transactions;

namespace Chu.Bank.Inc.Infrastructure.ExternalServices;

public class BrasilApiHolidayService : IHolidayService
{
    private readonly HttpClient _httpClient;

    public BrasilApiHolidayService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> IsHolidayAsync(DateTime date)
    {
        var year = date.Year;

        var holidays = await _httpClient.GetFromJsonAsync<List<HolidayDto>>($"https://brasilapi.com.br/api/feriados/v1/{year}");

        var isHoliday = holidays?.Any(h => DateTime.Parse(h.Date).Date == date.Date) == true;

        return isHoliday;
    }

    private record HolidayDto(string Date, string Name, string Type);
}