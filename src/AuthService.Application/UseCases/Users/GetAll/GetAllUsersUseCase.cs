using AuthService.Communication.Responses;
using AuthService.Domain.Repositories;
using AutoMapper;

namespace AuthService.Application.UseCases.Users.GetAll;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    public GetAllUsersUseCase(
        IUserRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseUsersJson> Execute()
    {
        var users = await _repository.GetAll();

        return new ResponseUsersJson
        {
            Users = _mapper.Map<List<ResponseShortUserJson>>(users)
        };
    }
}
