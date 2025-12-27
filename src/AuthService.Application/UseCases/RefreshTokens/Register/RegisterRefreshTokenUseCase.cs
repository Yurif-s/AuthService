using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using AuthService.Domain.Security.Tokens;
using AuthService.Exception.ExceptionsBase;

namespace AuthService.Application.UseCases.RefreshTokens.Register;

public class RegisterRefreshTokenUseCase : IRegisterRefreshTokenUseCase
{
    private readonly IRefreshTokenRepository _rtRepository;
    private readonly IUserRepository _userRepository;
    private readonly uint _refreshExpirationTimeMinutes;
    private readonly IRefreshTokenGenerator _generator;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterRefreshTokenUseCase(
        IRefreshTokenRepository repository,
        IUserRepository userRepository,
        uint refreshExpirationTimeMinutes,
        IRefreshTokenGenerator generator,
        IUnitOfWork unitOfWork)
    {
        _rtRepository = repository;
        _userRepository = userRepository;
        _refreshExpirationTimeMinutes = refreshExpirationTimeMinutes;
        _generator = generator; 
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(Guid userId)
    {
        var user = await _userRepository.GetById(userId);

        if (user is null)
            throw new NotFoundException("User not found.");

        var refreshToken = new RefreshToken
        {
            Token = _generator.GenerateRefreshToken(),
            ExpiresAt = DateTime.UtcNow.AddMinutes(_refreshExpirationTimeMinutes),
            Id = Guid.NewGuid(),
            UserId = userId
        };

        await _rtRepository.Add(refreshToken);
        await _unitOfWork.Commit();
    }
}
