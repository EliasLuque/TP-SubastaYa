using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Auction;
using SubastaYa.Aplication.Dtos.AuditLog;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.AuditLogs.Queries.GetAllQuery;

internal sealed class GetAllAuditLogHandler : IRequestHandler<GetAllAuditLogQuery, BaseResponse<IEnumerable<AuditLogResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllAuditLogHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<AuditLogResponseDto>>> Handle(GetAllAuditLogQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<AuditLogResponseDto>>();

        try
        {
            var auditLogs = await _unitOfWork.AuditLog.GetAllProjectedAsync<AuditLogResponseDto>(_mapper.ConfigurationProvider, cancellationToken);

            response.IsSuccess = true;
            response.Data = auditLogs;
            response.Message = "Audit logs retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error retrieving audit logs: {ex.Message}";
        }

        return response;
    }
}
