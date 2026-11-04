namespace TravelDNA.Infrastructure.Persistence;

internal sealed class UserCredentialRecord
{
    public Guid UserId { get; set; }

    public string PasswordHash { get; set; } = string.Empty;
}