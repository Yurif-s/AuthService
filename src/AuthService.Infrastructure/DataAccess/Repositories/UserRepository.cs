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

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<bool> Delete(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

        if (user is null) 
            return false;

        _dbContext.Users.Remove(user);
        return true;
    }

    public async Task<bool> EmailExist(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email == email);
    }

    public async Task<List<User>> GetAll()
    {
        return await _dbContext.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _dbContext.Users
            .Include(user => user.RefreshTokens)
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User?> GetById(Guid id)
    {
        return await _dbContext.Users
            .Include(user => user.RefreshTokens)
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<bool> Update(User user)
    {
        if (user is null)
            return false;

        _dbContext.Users.Update(user);
        return true;
    }
}
