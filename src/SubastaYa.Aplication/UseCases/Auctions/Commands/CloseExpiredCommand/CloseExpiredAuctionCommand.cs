using MediatR;
using SubastaYa.Aplication.Commons.Bases;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.CloseExpiredCommand;

public class CloseExpiredAuctionCommand : IRequest<BaseResponse<int>>
{
}
