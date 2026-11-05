using System.ComponentModel.DataAnnotations;
using TravelDNA.Application.Abstractions.Authentication;
using TravelDNA.Application.Abstractions.Persistence;
using TravelDNA.Domain.Entities;

namespace TravelDNA.Application.Authentication;

public sealed class AuthService
{
    private readonly IUserRepository _users;
    private readonly IUserCredentialRepository _credentials;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository users,
        IUserCredentialRepository credentials,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _users = users;
        _credentials = credentials;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> RegisterAsync(
        string email,
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(email) ||
            !new EmailAddressAttribute().IsValid(email.Trim()))
        {
            throw new ArgumentException("A valid email is required.");
        }

        if (string.IsNullOrWhiteSpace(username) ||
            username.Trim().Length is < 3 or > 30)
        {
            throw new ArgumentException(
                "Username must contain between 3 and 30 characters.");
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length < 12)
        {
            throw new ArgumentException(
                "Password must contain at least 12 characters.");
        }

        var normalizedEmail = email.Trim().ToLowerInvariant();
        var normalizedUsername = username.Trim().ToLowerInvariant();

        if (await _users.GetByEmailAsync(
                normalizedEmail, cancellationToken) is not null)
        {
            throw new DuplicateUserException("Email is already registered.");
        }

        if (await _users.UsernameExistsAsync(
                normalizedUsername, cancellationToken))
        {
            throw new DuplicateUserException("Username is already taken.");
        }

        var user = new User(
            Guid.NewGuid(),
            normalizedEmail,
            normalizedUsername);

        var passwordHash = _passwordHasher.HashPassword(password);

        _users.Add(user);
        _credentials.Add(user.Id, passwordHash);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponse(
            _tokenService.CreateToken(user),
            user.Id,
            user.Email,
            user.Username);
    }

    public async Task<AuthResponse?> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        var user = await _users.GetByEmailAsync(
            email, cancellationToken);

        if (user is null || !user.IsActive)
            return null;

        var passwordHash = await _credentials.GetPasswordHashAsync(
            user.Id, cancellationToken);

        if (passwordHash is null ||
            !_passwordHasher.VerifyPassword(password, passwordHash))
        {
            return null;
        }

        return new AuthResponse(
            _tokenService.CreateToken(user),
            user.Id,
            user.Email,
            user.Username);
    }
}

public sealed class DuplicateUserException : Exception
{
    public DuplicateUserException(string message) : base(message)
    {
    }
}