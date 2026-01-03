using AuthService.Application.AutoMapper;
using AuthService.Application.UseCases.Login;
using AuthService.Application.UseCases.Users.Delete;
using AuthService.Application.UseCases.Users.GetAll;
using AuthService.Application.UseCases.Users.GetById;
using AuthService.Application.UseCases.Users.Register;
using AuthService.Application.UseCases.Users.Update;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddAutoMapper(services);
    }
    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
        services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
    }
    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(config => config.AddProfile<AutoMapping>());
    }
}
