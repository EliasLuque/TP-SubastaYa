using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.TransactionLedger;

namespace SubastaYa.Aplication.UseCases.TransactionLedger.Queries.GetAllQuery;

public class GetAllTransactionQuery : IRequest<BaseResponse<IEnumerable<TransactionLedgerResponseDto>>>
{
}
