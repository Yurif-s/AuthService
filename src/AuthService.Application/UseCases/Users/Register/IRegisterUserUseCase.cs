using AuthService.Communication.Requests;
using AuthService.Communication.Responses;

namespace AuthService.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request); 
}
