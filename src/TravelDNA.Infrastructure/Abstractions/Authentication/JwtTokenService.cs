using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TravelDNA.Application.Abstractions.Authentication;
using TravelDNA.Domain.Entities;

namespace TravelDNA.Infrastructure.Authentication;

public sealed class JwtTokenService : ITokenService
{
    private readonly string _issuer;
    private readonly string _audience;
    private readonly SymmetricSecurityKey _signingKey;

    public JwtTokenService(IConfiguration configuration)
    {
        _issuer = configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException(
                "Jwt:Issuer configuration is missing.");

        _audience = configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException(
                "Jwt:Audience configuration is missing.");

        var signingKey = configuration["Jwt:SigningKey"];

        if (string.IsNullOrWhiteSpace(signingKey) ||
            Encoding.UTF8.GetByteCount(signingKey) < 32)
        {
            throw new InvalidOperationException(
                "Jwt:SigningKey must contain at least 32 UTF-8 bytes.");
        }

        _signingKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(signingKey));
    }

    public string CreateToken(User user)
    {
        var now = DateTime.UtcNow;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("username", user.Username),
            new Claim(
                JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
        };

        var credentials = new SigningCredentials(
            _signingKey,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            notBefore: now,
            expires: now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}