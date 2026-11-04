namespace TravelDNA.Application.Abstractions.Persistence;

public interface IUserCredentialRepository
{
    Task<string?> GetPasswordHashAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    void Add(Guid userId, string passwordHash);
}