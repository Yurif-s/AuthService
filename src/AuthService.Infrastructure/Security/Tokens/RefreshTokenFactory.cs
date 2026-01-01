using AuthService.Domain.Entities;
using AuthService.Domain.Security.Tokens;

namespace AuthService.Infrastructure.Security.Tokens;

internal class RefreshTokenFactory : IRefreshTokenFactory
{
    private readonly uint _refreshExpirationTimeMinutes;
    public RefreshTokenFactory(uint refreshExpirationTimeMinutes)
    {
        _refreshExpirationTimeMinutes = refreshExpirationTimeMinutes;
    }
    public async Task<RefreshToken> Create(Guid userId)
    {
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_refreshExpirationTimeMinutes),
            IsRevoked = false
        };
    }
}
