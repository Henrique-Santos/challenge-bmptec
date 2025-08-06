using Chu.Bank.Inc.Domain.Entities.Users;

namespace Chu.Bank.Inc.Api.Extensions;

public class AspNetUser : IUser
{
    private readonly IHttpContextAccessor _accessor;

    public AspNetUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string Username => _accessor.HttpContext?.User.Identity?.Name ?? string.Empty;

    public bool IsAuthenticated()
    {
        return _accessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }
}