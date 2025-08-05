using System.ComponentModel.DataAnnotations;

namespace Chu.Bank.Inc.Api.Contracts.Authentications;

public record LoginRequest
{
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must have between {2} and {1} caracteres.")]
    public string Password { get; set; } = string.Empty;
}