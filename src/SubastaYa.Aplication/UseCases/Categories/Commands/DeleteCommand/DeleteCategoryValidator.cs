using FluentValidation;

namespace SubastaYa.Aplication.UseCases.Categories.Commands.DeleteCommand;

public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");
    }
}
