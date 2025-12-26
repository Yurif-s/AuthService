using AuthService.Domain.Entities;
using AuthService.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Infrastructure.Security.Tokens;

internal class AccessTokenGenerator : IAccessTokenGenerator
{
    private readonly uint _jwtExpirationTimeMinutes;
    private readonly string _signingKey;
    public AccessTokenGenerator(uint JwtExpirationTimeMinutes, string signingKey)
    {
        _jwtExpirationTimeMinutes = JwtExpirationTimeMinutes;
        _signingKey = signingKey;
    }
    public string Generate(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Sid, user.Id.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var accessToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(accessToken);
    }
    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_signingKey);

        return new SymmetricSecurityKey(key);
    }
}
