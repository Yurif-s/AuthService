using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;

namespace AuthService.Infrastructure.DataAccess.Repositories;

internal class RefreshTokenRepository : IRefreshTokenRepository
{
    public Task Add(RefreshToken refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<RefreshToken?> GetByToken(string token)
    {
        throw new NotImplementedException();
    }

    public Task<List<RefreshToken>> GetByUser(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Remove(RefreshToken refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Revoke(RefreshToken refreshToken)
    {
        throw new NotImplementedException();
    }
}
