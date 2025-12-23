using AuthService.Communication.Requests;
using AuthService.Communication.Responses;

namespace AuthService.Application.UseCases.Login;

public interface ILoginUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
