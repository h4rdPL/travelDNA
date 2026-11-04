using Microsoft.EntityFrameworkCore;
using TravelDNA.Application.Abstractions.Persistence;
using TravelDNA.Domain.Entities;

namespace TravelDNA.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Users
            .FirstOrDefaultAsync(
                user => user.Id == id,
                cancellationToken);
    }

    public Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        return _dbContext.Users
            .FirstOrDefaultAsync(
                user => user.Email == normalizedEmail,
                cancellationToken);
    }

    public Task<bool> UsernameExistsAsync(
        string username,
        CancellationToken cancellationToken = default)
    {
        var normalizedUsername = username.Trim().ToLowerInvariant();

        return _dbContext.Users.AnyAsync(
            user => user.Username == normalizedUsername,
            cancellationToken);
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }
}