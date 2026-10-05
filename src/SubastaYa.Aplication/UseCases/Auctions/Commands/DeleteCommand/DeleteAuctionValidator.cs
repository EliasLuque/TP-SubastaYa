using FluentValidation;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.DeleteCommand;

public class DeleteAuctionValidator : AbstractValidator<DeleteAuctionCommand>
{
    public DeleteAuctionValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Auction ID must be greater than zero.");

    }
}
