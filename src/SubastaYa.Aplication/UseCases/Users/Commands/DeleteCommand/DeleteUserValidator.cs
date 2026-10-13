using FluentValidation;

namespace SubastaYa.Aplication.UseCases.Users.Commands.DeleteCommand;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");
    }
}
