using AuthService.Domain.Entities;

namespace AuthService.Domain.Security.Tokens;

public interface IRefreshTokenFactory
{
    Task<RefreshToken> Create(Guid userId);
}
