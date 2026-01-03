using AuthService.Communication.Responses;

namespace AuthService.Application.UseCases.Users.GetAll;

public interface IGetAllUsersUseCase
{
    Task<ResponseUsersJson> Execute();
}
