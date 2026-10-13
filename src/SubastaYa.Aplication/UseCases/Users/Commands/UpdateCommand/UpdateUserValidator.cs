using FluentValidation;

namespace SubastaYa.Aplication.UseCases.Users.Commands.UpdateCommand;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("User id must be greater than zero.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("User name cannot be empty.")
            .MaximumLength(150).WithMessage("User name cannot exceed 150 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("User email cannot be empty.")
            .MaximumLength(256).WithMessage("User email cannot exceed 256 characters.")
            .EmailAddress().WithMessage("Email must be a valid address.");
    }
}
