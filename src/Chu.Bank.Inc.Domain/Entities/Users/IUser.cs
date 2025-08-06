namespace Chu.Bank.Inc.Domain.Entities.Users;

public interface IUser
{
    string Username { get; }
    bool IsAuthenticated();
}