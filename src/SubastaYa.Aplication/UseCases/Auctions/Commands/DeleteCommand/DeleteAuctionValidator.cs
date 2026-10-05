using FluentValidation;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.DeleteCommand;

public class DeleteAuctionValidator : AbstractValidator<DeleteAuctionCommand>
{
    public DeleteAuctionValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Auction ID must be greater than zero.");

        RuleFor(x => x.SellerId)
            .GreaterThan(0).WithMessage("Seller ID must be greater than zero.");
    }
}
