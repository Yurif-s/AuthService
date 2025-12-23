using AuthService.Communication.Requests;
using AuthService.Communication.Responses;
using AuthService.Domain.Repositories;
using AuthService.Domain.Security.Cryptography;
using AuthService.Domain.Security.Tokens;
using AuthService.Exception.ExceptionsBase;

namespace AuthService.Application.UseCases.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    public LoginUseCase(IUserRepository repository, IPasswordHasher passwordHasher, IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _accessTokenGenerator = accessTokenGenerator;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetByEmail(request.Email);

        if (user is null)
        {
            throw new InvalidLoginException();
        }

        var passwordMatch = _passwordHasher.Verify(request.Password, user.Password);

        if (!passwordMatch)
            throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user)
        };
    }
}
