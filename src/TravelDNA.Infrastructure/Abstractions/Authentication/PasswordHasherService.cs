
using Microsoft.AspNetCore.Identity;
using TravelDNA.Application.Abstractions.Authentication;

namespace TravelDNA.Infrastructure.Authentication;

public sealed class PasswordHasherService : IPasswordHasher
{
    private readonly PasswordHasher<string> _passwordHasher = new();

    public string HashPassword(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        return _passwordHasher.HashPassword(
            "test_password",
            password);
    }

    public bool VerifyPassword(
        string password,
        string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);

        var result = _passwordHasher.VerifyHashedPassword(
            "TravelDNA",
            passwordHash,
            password);

        return result != PasswordVerificationResult.Failed;
    }
}
