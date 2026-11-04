namespace TravelDNA.Domain.Entities;

public sealed class UserProfile
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public string DisplayName { get; private set; } = string.Empty;

    public string? Bio { get; private set; }

    public string? AvatarUrl { get; private set; }

    public string? HomeCountryCode { get; private set; }

    public string PreferredLanguage { get; private set; } = "en";

    public DateTime CreatedAtUtc { get; private set; }

    public DateTime? UpdatedAtUtc { get; private set; }

    private UserProfile() { }

    public UserProfile(Guid userId, string displayName)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID is required.", nameof(userId));

        if (string.IsNullOrWhiteSpace(displayName))
            throw new ArgumentException("Display name is required.", nameof(displayName));

        Id = Guid.NewGuid();
        UserId = userId;
        DisplayName = displayName.Trim();
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void Update(
        string displayName,
        string? bio,
        string? avatarUrl,
        string? homeCountryCode,
        string preferredLanguage)
    {
        if (string.IsNullOrWhiteSpace(displayName))
            throw new ArgumentException("Display name is required.", nameof(displayName));

        DisplayName = displayName.Trim();
        Bio = bio?.Trim();
        AvatarUrl = avatarUrl;
        HomeCountryCode = homeCountryCode?.Trim().ToUpperInvariant();
        PreferredLanguage = string.IsNullOrWhiteSpace(preferredLanguage)
            ? "en"
            : preferredLanguage.Trim();

        UpdatedAtUtc = DateTime.UtcNow;
    }
}