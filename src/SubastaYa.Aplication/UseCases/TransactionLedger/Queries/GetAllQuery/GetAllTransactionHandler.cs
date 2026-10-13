using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.TransactionLedger;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.TransactionLedger.Queries.GetAllQuery;

internal sealed class GetAllTransactionHandler : IRequestHandler<GetAllTransactionQuery, BaseResponse<IEnumerable<TransactionLedgerResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllTransactionHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<TransactionLedgerResponseDto>>> Handle(GetAllTransactionQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<TransactionLedgerResponseDto>>();

        try
        {
            var transactions = await _unitOfWork.TransactionLedger.GetAllProjectedAsync<TransactionLedgerResponseDto>(_mapper.ConfigurationProvider, cancellationToken);

            response.IsSuccess = true;
            response.Data = transactions;
            response.Message = "Transactions retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = null;
            response.Message = $"Error retireving transactions: {ex.Message}";
        }

        return response;
    }
}
