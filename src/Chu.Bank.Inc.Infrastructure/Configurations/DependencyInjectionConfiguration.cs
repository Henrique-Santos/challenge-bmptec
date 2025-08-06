using Chu.Bank.Inc.Domain.Repositories;
using Chu.Bank.Inc.Domain.Services.Transactions;
using Chu.Bank.Inc.Infrastructure.Data;
using Chu.Bank.Inc.Infrastructure.Data.Repositories;
using Chu.Bank.Inc.Infrastructure.ExternalServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chu.Bank.Inc.Infrastructure.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        // Domain Services
        services.AddHttpClient<IHolidayService, BrasilApiHolidayService>();

        // Caching
        services.AddMemoryCache();

        return services;
    }
}