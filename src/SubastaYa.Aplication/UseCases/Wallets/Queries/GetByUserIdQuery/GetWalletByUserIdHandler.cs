using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Wallet;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Wallets.Queries.GetByUserIdQuery;

internal sealed class GetWalletByUserIdHandler : IRequestHandler<GetWalletByUserIdQuery, BaseResponse<WalletResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetWalletByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<WalletResponseDto>> Handle(GetWalletByUserIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<WalletResponseDto>();

        try
        {
            var wallet = await _unitOfWork.Wallet.GetByUserIdAsync(request.UserId, cancellationToken);

            if (wallet == null)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = $"Wallet not found for user ID {request.UserId}";
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<WalletResponseDto>(wallet);
            response.Message = "Wallet retrieved successfully";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = null;
            response.Message = $"Error retrieving wallet for user ID {request.UserId}: {ex.Message}";
        }

        return response;
    }
}
