using FluentValidation;

namespace SubastaYa.Aplication.UseCases.Users.Commands.LoginCommand;

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Email must be a valid address.")
            .MaximumLength(256).WithMessage("Email cannot exceed 256 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("User password cannot be empty.")
            .MinimumLength(8).WithMessage("User password must be at least 8 characters long.");
    }
}
