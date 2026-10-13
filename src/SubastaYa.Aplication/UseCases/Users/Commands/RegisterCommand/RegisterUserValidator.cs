using FluentValidation;

namespace SubastaYa.Aplication.UseCases.Users.Commands.RegisterCommand;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("User name cannot be empty.")
            .MaximumLength(150).WithMessage("User name cannot exceed 100 characters ");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("User email cannot be empty.")
            .EmailAddress().WithMessage("A valid email address is required")
            .MaximumLength(256).WithMessage("Email cannot exceed 256 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }
}
