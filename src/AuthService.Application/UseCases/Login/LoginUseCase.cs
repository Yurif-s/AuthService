using AuthService.Communication.Requests;
using AuthService.Communication.Responses;
using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using AuthService.Domain.Security.Cryptography;
using AuthService.Domain.Security.Tokens;
using AuthService.Exception.ExceptionsBase;

namespace AuthService.Application.UseCases.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IRefreshTokenRepository _rtRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IRefreshTokenFactory _refreshTokenFactory;
    private readonly IUnitOfWork _unitOfWork;
    public LoginUseCase(
        IRefreshTokenRepository rtRepository,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IAccessTokenGenerator accessTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        IRefreshTokenFactory refreshTokenFactory,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _rtRepository = rtRepository;
        _passwordHasher = passwordHasher;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenFactory = refreshTokenFactory;
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseLoggedUserJson> Execute(RequestLoginJson request)
    {
        var user = await Validate(request);

        var refreshToken = await _refreshTokenFactory.Create(user.Id);
        refreshToken.Token = _refreshTokenGenerator.GenerateRefreshToken();

        await _rtRepository.Add(refreshToken);
        await _unitOfWork.Commit();

        return new ResponseLoggedUserJson
        {
            Name = user.Name,
            AccessToken = _accessTokenGenerator.Generate(user),
            RefreshToken = refreshToken.Token
        };
    }
    private async Task<User> Validate(RequestLoginJson request)
    {
        var user = await _userRepository.GetByEmail(request.Email);

        if (user is null)
        {
            throw new InvalidLoginException();
        }

        var passwordMatch = _passwordHasher.Verify(request.Password, user.Password);

        if (!passwordMatch)
            throw new InvalidLoginException();

        return user;
    }
}
