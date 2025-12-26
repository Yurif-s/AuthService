using AuthService.Domain.Security.Tokens;
using System.Security.Cryptography;

namespace AuthService.Infrastructure.Security.Tokens;

public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        var rng = RandomNumberGenerator.Create();
        
        rng.GetBytes(randomNumber);
        var token = Convert.ToBase64String(randomNumber);

        return token;
    }
}
