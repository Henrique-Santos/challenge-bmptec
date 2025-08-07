using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chu.Bank.Inc.Infrastructure.Data;

public class ApplicationDbHelper
{
    public static async Task EnsureDatabaseCreatedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        if (context.Database.CanConnect())
        {
            await SeedApplicationTables(context);
        }
    }

    private static async Task SeedApplicationTables(ApplicationDbContext context)
    {
        if (!await context.Accounts.AnyAsync())
        {
            var accounts = Seeder.GetAccounts();
            await context.Accounts.AddRangeAsync(accounts);
            await context.SaveChangesAsync();

            if (!await context.Transactions.AnyAsync())
            {
                var transactions = Seeder.GetTransactions();
                await context.Transactions.AddRangeAsync(transactions);
                await context.SaveChangesAsync();
            }
        }
    }
}