using System.ComponentModel.DataAnnotations;

namespace TravelDNA.Api.Contracts.Auth;

public sealed record RegisterRequest(
    [property: Required, EmailAddress, MaxLength(254)]
    string Email,

    [property: Required, MinLength(3), MaxLength(30)]
    string Username,

    [property: Required, MinLength(12), MaxLength(128)]
    string Password);