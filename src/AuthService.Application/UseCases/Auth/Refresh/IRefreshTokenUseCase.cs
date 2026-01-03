using AuthService.Communication.Requests;
using AuthService.Communication.Responses;
using AuthService.Domain.Entities;

namespace AuthService.Application.UseCases.Auth.Refresh;

public interface IRefreshTokenUseCase
{
    Task<ResponseRefreshJson> Execute(RequestRefreshJson request);
}
