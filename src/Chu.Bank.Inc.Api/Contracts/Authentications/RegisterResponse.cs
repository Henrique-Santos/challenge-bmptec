namespace Chu.Bank.Inc.Api.Contracts.Authentications;

public record RegisterResponse
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public double ExpiresIn { get; set; }
}