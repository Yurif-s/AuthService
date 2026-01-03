
using AuthService.Domain.Repositories;
using AuthService.Exception.ExceptionsBase;

namespace AuthService.Application.UseCases.Users.Delete;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteUserUseCase(
        IUserRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(Guid id)
    {
        var result = await _repository.Delete(id);

        if (result is false)
            throw new NotFoundException("User not found.");

        await _unitOfWork.Commit();
    }
}
