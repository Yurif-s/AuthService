using AuthService.Domain.Entities;

namespace AuthService.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}
