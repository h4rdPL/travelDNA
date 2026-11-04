using TravelDNA.Domain.Entities;

namespace TravelDNA.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<bool> UsernameExistsAsync(
        string username,
        CancellationToken cancellationToken = default);

    void Add(User user);
}