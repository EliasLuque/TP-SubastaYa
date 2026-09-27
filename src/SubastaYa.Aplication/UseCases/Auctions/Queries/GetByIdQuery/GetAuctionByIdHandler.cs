using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Auction;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Auctions.Queries.GetByIdQuery;

internal sealed class GetAuctionByIdHandler : IRequestHandler<GetAuctionByIdQuery, BaseResponse<AuctionResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAuctionByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<AuctionResponseDto>> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<AuctionResponseDto>();

        try
        {
            var existAuction = await _unitOfWork.Auction.GetByIdAsync(request.Id);

            if (existAuction == null)
            {
                response.IsSuccess = false;
                response.Message = $"Auction with Id {request.Id} not found.";
                return response;
            }

            var auction = _mapper.Map<AuctionResponseDto>(existAuction);
            response.IsSuccess = true;
            response.Data = auction;
            response.Message = "Auction retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
