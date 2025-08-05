namespace Chu.Bank.Inc.Api.Contracts.Authentications;

public record LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public double ExpiresIn { get; set; }
}