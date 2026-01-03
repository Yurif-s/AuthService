using AuthService.Communication.Requests;
using AuthService.Domain.Repositories;
using AuthService.Exception.ExceptionsBase;

namespace AuthService.Application.UseCases.Users.Update;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateUserUseCase(
        IUserRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(Guid id, RequestUpdateUserJson request)
    {
        Validate(request);

        var user = await _repository.GetById(id);

        if (user is null)
            throw new NotFoundException("User not found.");

        user.Name = request.Name;
        user.Email = request.Email;

        _repository.Update(user);
        await _unitOfWork.Commit();
    }
    private void Validate(RequestUpdateUserJson request)
    {
        var validator = new UpdateUserValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
