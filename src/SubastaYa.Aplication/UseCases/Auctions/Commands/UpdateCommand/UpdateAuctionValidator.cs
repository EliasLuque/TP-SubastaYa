using FluentValidation;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.UpdateCommand;

public class UpdateAuctionValidator : AbstractValidator<UpdateAuctionCommand>
{
    public UpdateAuctionValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greather than zero");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description cannot be empty")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 charecers");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Image URL cannot be empty")
            .MaximumLength(2048).WithMessage("Image URL cannot exceed 2048 characters")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("Must provide a valid URL");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category Id must be greather than zero");

        RuleFor(x => x.BasePrice)
            .GreaterThan(0).WithMessage("Base price must be greather than zero");

        RuleFor(x => x.MinimumIncrement)
            .GreaterThan(0).WithMessage("Minimum increment must be greather than zero");

        RuleFor(x => x.StartDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Start Date must be in the future");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date.");

    }
}
