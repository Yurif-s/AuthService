using AuthService.Communication.Requests;
using AuthService.Communication.Responses;
using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using AuthService.Domain.Security.Cryptography;
using AuthService.Domain.Security.Tokens;
using AuthService.Exception.ExceptionsBase;
using AutoMapper;

namespace AuthService.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IRefreshTokenFactory _refreshTokenFactory;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    public RegisterUserUseCase(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPasswordHasher passwordHasher,
        IRefreshTokenGenerator refreshTokenGenerator,
        IRefreshTokenFactory refreshTokenFactory,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenFactory = refreshTokenFactory;
        _accessTokenGenerator = accessTokenGenerator;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);
        user.Id = Guid.NewGuid();
        user.Password = _passwordHasher.Hash(request.Password);
        user.IsActive = true;

        var refreshToken = await _refreshTokenFactory.Create(user.Id);
        refreshToken.Token = _refreshTokenGenerator.GenerateRefreshToken();

        await _userRepository.Add(user);
        await _refreshTokenRepository.Add(refreshToken);

        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user),
        };
    }
    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var emailExists = await _userRepository.EmailExist(request.Email);

        if (emailExists)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Email already registered."));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
