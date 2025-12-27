using AuthService.Domain.Entities;

namespace AuthService.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task Add(RefreshToken refreshToken);
    Task<bool> Revoke(Guid id);
    Task<RefreshToken?> GetByToken(string token);
    Task<List<RefreshToken>> GetByUser(Guid userId);
    Task<bool> Delete(Guid id);
}
