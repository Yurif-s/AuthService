using AuthService.Communication.Responses;

namespace AuthService.Application.UseCases.Users.GetById;

public interface IGetUserByIdUseCase
{
    Task<ResponseUserJson> Execute(Guid id);
}
