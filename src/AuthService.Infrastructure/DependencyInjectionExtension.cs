using AuthService.Domain.Repositories;
using AuthService.Domain.Security.Cryptography;
using AuthService.Domain.Security.Tokens;
using AuthService.Infrastructure.DataAccess;
using AuthService.Infrastructure.DataAccess.Repositories;
using AuthService.Infrastructure.Security.Cryptography;
using AuthService.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddToken(services, configuration);

        services.AddScoped<IPasswordHasher, PasswordHasher>();
    }
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(config => config.UseSqlServer(connectionString));
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }
    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var jwtExpirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var refreshExpirationTimeMinutes = configuration.GetValue<uint>("Settings:Refresh:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
        services.AddScoped<IAccessTokenGenerator, AccessTokenGenerator>();
    }
}
