using Microsoft.EntityFrameworkCore;
using TravelDNA.Domain.Entities;

namespace TravelDNA.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    internal DbSet<UserCredentialRecord> UserCredentials =>
        Set<UserCredentialRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(user => user.Id);

            entity.Property(user => user.Email)
                .HasMaxLength(254)
                .IsRequired();

            entity.HasIndex(user => user.Email)
                .IsUnique();

            entity.Property(user => user.Username)
                .HasMaxLength(30)
                .IsRequired();

            entity.HasIndex(user => user.Username)
                .IsUnique();

            entity.Property(user => user.EmailVerified)
                .IsRequired();

            entity.Property(user => user.IsActive)
                .IsRequired();

            entity.Property(user => user.CreatedAtUtc)
                .IsRequired();
        });

        modelBuilder.Entity<UserCredentialRecord>(entity =>
        {
            entity.ToTable("user_credentials");

            entity.HasKey(credential => credential.UserId);

            entity.Property(credential => credential.PasswordHash)
                .HasMaxLength(512)
                .IsRequired();

            entity.HasOne<User>()
                .WithOne()
                .HasForeignKey<UserCredentialRecord>(
                    credential => credential.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}