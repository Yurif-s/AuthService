using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.DataAccess.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;
    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

        if (user is null) 
            return false;

        _dbContext.Users.Remove(user);
        return true;
    }

    public async Task<bool> EmailExistAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email == email);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users
            .Include(user => user.RefreshTokens)
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users
            .Include(user => user.RefreshTokens)
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public void UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
    }
}
