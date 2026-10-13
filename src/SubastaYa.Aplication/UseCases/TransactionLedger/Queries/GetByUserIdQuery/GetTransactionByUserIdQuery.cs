using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.TransactionLedger;
using System.Text.Json.Serialization;

namespace SubastaYa.Aplication.UseCases.TransactionLedger.Queries.GetByIdQuery;

public class GetTransactionByUserIdQuery : IRequest<BaseResponse<IEnumerable<TransactionLedgerResponseDto>>>
{
    [JsonIgnore]
    public int UserId { get; set; }
}
