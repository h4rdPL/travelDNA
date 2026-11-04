namespace TravelDNA.Application.Authentication;

public sealed record AuthResponse(
    string AccessToken,
    Guid UserId,
    string Email,
    string Username);