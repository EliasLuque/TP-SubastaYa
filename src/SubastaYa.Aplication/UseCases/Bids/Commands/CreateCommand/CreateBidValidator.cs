using FluentValidation;

namespace SubastaYa.Aplication.UseCases.Bids.Commands.CreateCommand;

public class CreateBidValidator : AbstractValidator<CreateBidCommand>
{
    public CreateBidValidator()
    {
        RuleFor(x => x.AuctionId)
            .GreaterThan(0).WithMessage("Auction Id must be greather than zero.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Bid amount must be greather than zero.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("Unauthorized user");
    }
}
