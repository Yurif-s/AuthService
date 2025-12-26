using AuthService.Domain.Entities;

namespace AuthService.Domain.Security.Tokens;

public interface IRefreshTokenGenerator
{
    string GenerateRefreshToken();
}
