using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.AuditLog;

namespace SubastaYa.Aplication.UseCases.AuditLogs.Queries.GetByIdQuery;

public class GetAuditLogByIdQuery : IRequest<BaseResponse<AuditLogResponseDto>>
{
    public int Id { get; set; }
}
