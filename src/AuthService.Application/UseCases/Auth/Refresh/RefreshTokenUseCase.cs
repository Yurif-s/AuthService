using AuthService.Communication.Requests;
using AuthService.Communication.Responses;
using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using AuthService.Domain.Security.Tokens;
using AuthService.Exception.ExceptionsBase;

namespace AuthService.Application.UseCases.Auth.Refresh;

public class RefreshTokenUseCase : IRefreshTokenUseCase
{
    private readonly IRefreshTokenRepository _repository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    public RefreshTokenUseCase(
        IRefreshTokenRepository repository,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _accessTokenGenerator = accessTokenGenerator;
    }
    public async Task<ResponseRefreshJson> Execute(RequestRefreshJson request)
    {
        var refreshToken = await _repository.GetByToken(request.token);

        Validate(refreshToken!);

        var user = refreshToken!.User;

        return new ResponseRefreshJson
        {
            Token = _accessTokenGenerator.Generate(user)
        };
    }
    private void Validate(RefreshToken refreshToken)
    {
        if (refreshToken is null)
            throw new UnauthorizedException();

        if (refreshToken.IsRevoked is true)
            throw new UnauthorizedException();

        if (refreshToken.ExpiresAt <=  DateTime.UtcNow)
            throw new UnauthorizedException();
    }
}
