using System.ComponentModel.DataAnnotations;

namespace TravelDNA.Api.Contracts.Auth;

public sealed record LoginRequest(
    [property: Required, EmailAddress, MaxLength(254)]
    string Email,

    [property: Required, MinLength(1), MaxLength(128)]
    string Password);