using System.Net.Http.Json;
using Chu.Bank.Inc.Domain.Services.Transactions;
using Microsoft.Extensions.Caching.Memory;

namespace Chu.Bank.Inc.Infrastructure.ExternalServices;

public class BrasilApiHolidayService : IHolidayService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;

    public BrasilApiHolidayService(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public async Task<bool> IsHolidayAsync(DateTime date)
    {
        var year = date.Year;

        var holidays = await _cache.GetOrCreateAsync($"holidays-{year}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
            return await _httpClient.GetFromJsonAsync<List<HolidayDto>>($"https://brasilapi.com.br/api/feriados/v1/{year}");
        });

        var isHoliday = holidays?.Any(h => DateTime.Parse(h.Date).Date == date.Date) == true;

        return isHoliday;
    }

    private record HolidayDto(string Date, string Name, string Type);
}