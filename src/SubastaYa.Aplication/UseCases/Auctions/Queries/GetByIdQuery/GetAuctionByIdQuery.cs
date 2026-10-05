using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Auction;
using SubastaYa.Aplication.Dtos.AuditLog;

namespace SubastaYa.Aplication.UseCases.Auctions.Queries.GetByIdQuery;

public class GetAuctionByIdQuery : IRequest<BaseResponse<AuditLogResponseDto>>
{
    public int Id { get; set; }
}
