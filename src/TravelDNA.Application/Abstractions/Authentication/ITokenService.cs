using TravelDNA.Domain.Entities;

namespace TravelDNA.Application.Abstractions.Authentication;

public interface ITokenService
{
    string CreateToken(User user);
}