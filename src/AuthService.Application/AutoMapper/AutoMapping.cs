using AuthService.Communication.Requests;
using AuthService.Communication.Responses;
using AuthService.Domain.Entities;
using AutoMapper;

namespace AuthService.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());
    }

    private void EntityToResponse()
    {
        CreateMap<User, ResponseRegisteredUserJson>();
        CreateMap<User, ResponseUserJson>();
    }
}
