using AuthService.Communication.Requests;
using AuthService.Communication.Responses;
using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using AuthService.Domain.Security.Cryptography;
using AuthService.Exception.ExceptionsBase;
using AutoMapper;

namespace AuthService.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    public RegisterUserUseCase(IUserRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestUserJson request)
    {
        Validate(request);

        var user = _mapper.Map<User>(request);
        user.Id = Guid.NewGuid();
        user.Password = _passwordHasher.Hash(request.Password);

        await _repository.Add(user);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredUserJson>(user);
    }
    private void Validate(RequestUserJson request)
    {
        var validator = new UserValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
