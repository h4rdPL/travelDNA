
namespace TravelDNA.Domain.Entities;

public sealed class User
{
    public Guid Id { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public string Username { get; private set; } = string.Empty;

    public bool EmailVerified { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public DateTime? UpdatedAtUtc { get; private set; }

    private User() { }

    public User(Guid id, string email, string username)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("User ID is required.", nameof(id));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username is required.", nameof(username));

        Id = id;
        Email = email.Trim().ToLowerInvariant();
        Username = username.Trim();
        EmailVerified = false;
        IsActive = true;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void ChangeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        Email = email.Trim().ToLowerInvariant();
        EmailVerified = false;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void ChangeUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username is required.", nameof(username));

        Username = username.Trim();
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void VerifyEmail()
    {
        EmailVerified = true;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Reactivate()
    {
        IsActive = true;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}
