using FluentValidation;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.CreateCommand;

public class CreateAuctionValidator : AbstractValidator<CreateAuctionCommand>
{
    public CreateAuctionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.ImageUrl)
            .MaximumLength(2048).WithMessage("Image URL must not exceed 2048 characters.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("Image URL must be a valid URL.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category is required.");

        RuleFor(x => x.BasePrice)
            .GreaterThan(0).WithMessage("Base price must be greater than zero.");

        RuleFor(x => x.MinimumIncrement)
            .GreaterThan(0).WithMessage("Minimum increment must be greater than zero.");

        RuleFor(x => x.StartDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Start date must be in the future.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date.");
    }
}
