using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.TransactionLedger;

namespace SubastaYa.Aplication.UseCases.TransactionLedger.Queries.GetByAuctionIdQuery;

public class GetTransactionByAuctionIdQuery : IRequest<BaseResponse<IEnumerable<TransactionLedgerResponseDto>>>
{
    public int AuctionId { get; set; }
}
