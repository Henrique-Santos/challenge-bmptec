using Chu.Bank.Inc.Domain.Entities.Accounts;
using Chu.Bank.Inc.Domain.Entities.Transactions;

namespace Chu.Bank.Inc.Infrastructure.Data;

public static class Seeder
{
    public static List<Account> GetAccounts()
    {
        return
        [
            new(Guid.Parse("728e087f-31d0-44ef-9092-4d0e65c581ee"), Guid.Parse("cc74a267-ed34-473d-a775-0a9d5e70f969"), 10000.0m),
            new(Guid.Parse("8b77a349-7518-4607-85c5-2fe7bbb0382a"), Guid.Parse("95ff55e0-f6da-4fb1-b4b8-6b42b145da77"), 15000.0m),
        ];
    }

    public static List<Transaction> GetTransactions()
    {
        return
        [
            new(Guid.Parse("728e087f-31d0-44ef-9092-4d0e65c581ee"), Guid.Parse("8b77a349-7518-4607-85c5-2fe7bbb0382a"), 1000.0m),
            new(Guid.Parse("8b77a349-7518-4607-85c5-2fe7bbb0382a"), Guid.Parse("728e087f-31d0-44ef-9092-4d0e65c581ee"), 5000.0m),
        ];
    }
}