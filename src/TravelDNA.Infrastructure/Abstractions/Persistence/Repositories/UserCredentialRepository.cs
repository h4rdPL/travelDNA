using Microsoft.EntityFrameworkCore;
using TravelDNA.Application.Abstractions.Persistence;

namespace TravelDNA.Infrastructure.Persistence.Repositories;

public sealed class UserCredentialRepository
    : IUserCredentialRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserCredentialRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string?> GetPasswordHashAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var credential = await _dbContext.UserCredentials
            .AsNoTracking()
            .FirstOrDefaultAsync(
                item => item.UserId == userId,
                cancellationToken);

        return credential?.PasswordHash;
    }

    public void Add(Guid userId, string passwordHash)
    {
        _dbContext.UserCredentials.Add(
            new UserCredentialRecord
            {
                UserId = userId,
                PasswordHash = passwordHash
            });
    }
}