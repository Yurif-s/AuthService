using AuthService.Communication.Responses;
using AuthService.Domain.Repositories;
using AuthService.Exception.ExceptionsBase;
using AutoMapper;

namespace AuthService.Application.UseCases.Users.GetById;

public class GetUserByIdUseCase : IGetUserByIdUseCase
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    public GetUserByIdUseCase(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseUserJson> Execute(Guid id)
    {
        var result = await _repository.GetById(id);
        if (result is null)
            throw new NotFoundException("User not found.");

        return _mapper.Map<ResponseUserJson>(result);
    }
}
