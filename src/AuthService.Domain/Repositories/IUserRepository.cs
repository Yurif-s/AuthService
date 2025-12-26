using AuthService.Domain.Entities;

namespace AuthService.Domain.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAll();
    Task<User?> GetByEmail(string email);
    Task<User?> GetById(Guid id);

    Task Add(User user);
    Task<bool> Update(User user);

    Task<bool> Delete(Guid id);

    Task<bool> EmailExist(string email);
}
