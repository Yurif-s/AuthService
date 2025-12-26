using AuthService.Domain.Entities;

namespace AuthService.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task Add(RefreshToken refreshToken);
    Task<bool> Revoke(RefreshToken refreshToken);
    Task<RefreshToken?> GetByToken(string token);
    Task<List<RefreshToken>> GetByUser(Guid userId);
    Task<bool> Remove(RefreshToken refreshToken);
}
