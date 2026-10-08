using FluentValidation;

namespace SubastaYa.Aplication.UseCases.Categories.Commands.UpdateCommand;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name cannot be empty")
            .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");

        RuleFor(x => x.IconUrl)
            .MaximumLength(2048).WithMessage("Icon URL cannot exceed 100 characters.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("Icon URL must be valid URL")
            .When(x => !string.IsNullOrEmpty(x.IconUrl));
    }
}
