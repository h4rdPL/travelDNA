using System.ComponentModel.DataAnnotations;

namespace TravelDNA.Api.Contracts.Auth;

public sealed record LoginRequest(
    [Required, EmailAddress, MaxLength(254)]
    string Email,

    [Required, MinLength(1), MaxLength(128)]
    string Password
);