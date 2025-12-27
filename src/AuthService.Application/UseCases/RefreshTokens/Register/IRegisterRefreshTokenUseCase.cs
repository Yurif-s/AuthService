namespace AuthService.Application.UseCases.RefreshTokens.Register;

 public interface IRegisterRefreshTokenUseCase
{
    Task Execute(Guid userId);
}
