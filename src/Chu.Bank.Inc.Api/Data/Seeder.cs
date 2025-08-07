using Microsoft.AspNetCore.Identity;

namespace Chu.Bank.Inc.Api.Data;

public static class Seeder
{
    public static List<ApplicationUser> GetUsers()
    {
        return
        [
            new ApplicationUser
            {
                Id = "cc74a267-ed34-473d-a775-0a9d5e70f969",
                UserName = "john_doe",
                Email = "john@gmail.com",
                EmailConfirmed = true,
                Password = "wq^2I#2wgN0GdIZ"
            },
            new ApplicationUser
            {
                Id = "95ff55e0-f6da-4fb1-b4b8-6b42b145da77",
                UserName = "mary_doe",
                Email = "mary@gmail.com",
                EmailConfirmed = true,
                Password = "u8aatbj45igfgP!"
            }
        ];
    }
}

public class ApplicationUser : IdentityUser
{
    public string Password { get; set; } = string.Empty;
}