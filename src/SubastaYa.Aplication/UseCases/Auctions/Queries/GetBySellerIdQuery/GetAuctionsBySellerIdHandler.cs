using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Auction;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Auctions.Queries.GetBySellerIdQuery;

internal sealed class GetAuctionsBySellerIdHandler : IRequestHandler<GetAuctionsBySellerIdQuery, BaseResponse<IEnumerable<AuctionResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAuctionsBySellerIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<AuctionResponseDto>>> Handle(GetAuctionsBySellerIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<AuctionResponseDto>>();

        try
        {
            var auctions = await _unitOfWork.Auction.GetBySellerIdAsync(request.SellerId, cancellationToken);

            response.IsSuccess = true;
            response.Data = _mapper.Map<IEnumerable<AuctionResponseDto>>(auctions);
            response.Message = "Auctions retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = null;
            response.Message = $"Error retieving auctions: {ex.Message}";
        }
        
        return response;
    }
}
