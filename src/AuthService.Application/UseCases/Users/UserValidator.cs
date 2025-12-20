using AuthService.Communication.Requests;
using FluentValidation;

namespace AuthService.Application.UseCases.Users;

public class UserValidator : AbstractValidator<RequestUserJson>
{
    public UserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Name is required.");
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }
}
