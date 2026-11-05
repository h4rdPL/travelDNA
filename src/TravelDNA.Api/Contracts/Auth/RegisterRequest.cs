using System.ComponentModel.DataAnnotations;

namespace TravelDNA.Api.Contracts.Auth;

public sealed class RegisterRequest
{
    [Required]
    [EmailAddress]
    [MaxLength(254)]
    public string Email { get; init; } = string.Empty;

    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string Username { get; init; } = string.Empty;

    [Required]
    [MinLength(3)]
    [MaxLength(128)]
    public string Password { get; init; } = string.Empty;
}