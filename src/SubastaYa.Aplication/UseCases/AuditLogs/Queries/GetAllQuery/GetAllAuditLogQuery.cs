using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Auction;
using SubastaYa.Aplication.Dtos.AuditLog;

namespace SubastaYa.Aplication.UseCases.AuditLogs.Queries.GetAllQuery;

public class GetAllAuditLogQuery : IRequest<BaseResponse<IEnumerable<AuditLogResponseDto>>>
{
}
