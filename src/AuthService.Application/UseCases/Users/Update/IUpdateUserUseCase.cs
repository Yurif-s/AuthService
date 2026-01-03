using AuthService.Communication.Requests;

namespace AuthService.Application.UseCases.Users.Update;

public interface IUpdateUserUseCase
{
    Task Execute(Guid id, RequestUpdateUserJson request);
}
