using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.TransactionLedger;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.TransactionLedger.Queries.GetByAuctionIdQuery;

internal sealed class GetTransactionByAuctionIdHandler : IRequestHandler<GetTransactionByAuctionIdQuery, BaseResponse<IEnumerable<TransactionLedgerResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTransactionByAuctionIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<TransactionLedgerResponseDto>>> Handle(GetTransactionByAuctionIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<TransactionLedgerResponseDto>>();

        try
        {
            var transactions = await _unitOfWork.TransactionLedger.GetAllByAuctionIdAsync(request.AuctionId, cancellationToken);

            response.IsSuccess = true;
            response.Data = _mapper.Map<IEnumerable<TransactionLedgerResponseDto>>(transactions);
            response.Message = "Transactions retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = null;
            response.Message = $"Error retrieving transactions for auction ID {request.AuctionId}: {ex.Message}";
        }

        return response;
    }
}
