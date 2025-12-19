namespace AuthService.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}
