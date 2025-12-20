using AuthService.Application.AutoMapper;
using AuthService.Application.UseCases.Users.Register;
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
    }
    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(config => config.AddProfile(typeof(AutoMapping)));
    }
}
