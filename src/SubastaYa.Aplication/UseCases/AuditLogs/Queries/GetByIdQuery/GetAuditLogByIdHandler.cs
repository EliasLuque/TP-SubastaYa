using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.AuditLog;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.AuditLogs.Queries.GetByIdQuery;

internal sealed class GetAuditLogByIdHandler : IRequestHandler<GetAuditLogByIdQuery, BaseResponse<AuditLogResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAuditLogByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<AuditLogResponseDto>> Handle(GetAuditLogByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<AuditLogResponseDto>();

        try
        {
            var auditLog = await _unitOfWork.AuditLog.GetByIdProjectedAsync<AuditLogResponseDto>(request.Id, _mapper.ConfigurationProvider, cancellationToken);

            if(auditLog == null)
            {
                response.IsSuccess = false;
                response.Message = $"Audit log with ID {request.Id} not found";
                return response;
            }
            
            response.IsSuccess = true;
            response.Data = auditLog;
            response.Message = $"Audit log with ID {request.Id} retrieved successfully";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error retrieving audit log with ID {request.Id}: {ex.Message}";
        }

        return response;
    }
}
