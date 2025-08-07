using Chu.Bank.Inc.Domain.Entities.Accounts;
using Chu.Bank.Inc.Domain.Entities.Transactions;

namespace Chu.Bank.Inc.Infrastructure.Data;

public static class Seeder
{
    public static List<Account> GetAccounts()
    {
        return
        [
            new(Guid.Parse("cc74a267-ed34-473d-a775-0a9d5e70f969"), 10000.0m),
            new(Guid.Parse("95ff55e0-f6da-4fb1-b4b8-6b42b145da77"), 15000.0m),
        ];
    }

    public static List<Transaction> GetTransactions()
    {
        return
        [
            new(Guid.Parse("cc74a267-ed34-473d-a775-0a9d5e70f969"), Guid.Parse("95ff55e0-f6da-4fb1-b4b8-6b42b145da77"), 1000.0m),
            new(Guid.Parse("95ff55e0-f6da-4fb1-b4b8-6b42b145da77"), Guid.Parse("cc74a267-ed34-473d-a775-0a9d5e70f969"), 5000.0m),
        ];
    }
}