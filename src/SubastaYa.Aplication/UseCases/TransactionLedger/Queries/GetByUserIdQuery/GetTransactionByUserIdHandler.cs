using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.TransactionLedger;
using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Aplication.UseCases.Wallets.Queries.GetByUserIdQuery;

namespace SubastaYa.Aplication.UseCases.TransactionLedger.Queries.GetByIdQuery;

internal sealed class GetTransactionByUserIdHandler : IRequestHandler<GetTransactionByUserIdQuery, BaseResponse<IEnumerable<TransactionLedgerResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTransactionByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<TransactionLedgerResponseDto>>> Handle(GetTransactionByUserIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<TransactionLedgerResponseDto>>();

        try
        {
            var wallet = await _unitOfWork.Wallet.GetByUserIdAsync(request.UserId, cancellationToken);

            if (wallet == null)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = "Wallet not found.";
                return response;
            }

            var transactions = await _unitOfWork.TransactionLedger.GetAllByWalletIdAsync(wallet.Id, cancellationToken);

            response.IsSuccess = true;
            response.Data = _mapper.Map<IEnumerable<TransactionLedgerResponseDto>>(transactions);
            response.Message = "Transactions retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = null;
            response.Message = $"Error retrieving transaction: {ex.Message}";
        }

        return response;
    }
}
