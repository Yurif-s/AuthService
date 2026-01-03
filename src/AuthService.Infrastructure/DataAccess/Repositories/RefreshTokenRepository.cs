using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.DataAccess.Repositories;

internal class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _dbContext;
    public RefreshTokenRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(RefreshToken refreshToken)
    {
        await _dbContext.RefreshTokens.AddAsync(refreshToken);
    }

    public async Task<RefreshToken?> GetByToken(string token)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .Include(rt => rt.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.Token == token);

        return refreshToken;
    }

    public async Task<List<RefreshToken>> GetByUser(Guid userId)
    {
        var refreshTokens = await _dbContext.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();

        return refreshTokens;
    }

    public async Task<bool> Delete(Guid id)
    {
        var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Id == id);

        if (refreshToken is null)
            return false;

        _dbContext.RefreshTokens.Remove(refreshToken);
        return true;
    }

    public async Task<bool> Revoke(Guid id)
    {
        var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Id == id);

        if (refreshToken is null)
            return false;

        refreshToken.IsRevoked = true;

        return true;
    }
}
