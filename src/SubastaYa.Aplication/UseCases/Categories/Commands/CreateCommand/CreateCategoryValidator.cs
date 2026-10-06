using FluentValidation;

namespace SubastaYa.Aplication.UseCases.Categories.Commands.CreateCommand;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name cannot be empty")
            .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters");

        RuleFor(x => x.IconUrl)
            .MaximumLength(2048).WithMessage("Icon URL cannot exceed 2048 characters.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("Icon URL must be a valid URL.")
            .When(x => !string.IsNullOrEmpty(x.IconUrl));
    }
}
