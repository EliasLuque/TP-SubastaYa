using MediatR;
using SubastaYa.Aplication.Commons.Bases;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.DeleteCommand;

public class DeleteAuctionCommand : IRequest<BaseResponse<bool>>
{
    public int Id { get; set; }
}
