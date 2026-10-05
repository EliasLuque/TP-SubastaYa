using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.UseCases.Bids.Commands.CreateCommand;

internal sealed class CreateBidHandler : IRequestHandler<CreateBidCommand, BaseResponse<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBidHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<int>> Handle(CreateBidCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<int>();

        try
        {
            var auction = await _unitOfWork.Auction.GetByIdAsync(request.AuctionId);
            if (auction == null)
            {
                response.IsSuccess = false;
                response.Message = "Auction not found.";
                return response;
            }

            if (auction.Status != AuctionStatus.ACTIVE)
            {
                response.IsSuccess = false;
                response.Message = "Only active auctions can have bids.";
                return response;
            }

            if(request.Amount <= auction.CurrentPrice)
            {
                response.IsSuccess = false;
                response.Message = "The bid amount must be greather than the current price.";
                return response;
            }

            if(auction.SellerId == request.UserId)
            {
                response.IsSuccess = false;
                response.Message = "The seller cannot bid.";
                return response;
            }

            var bid = _mapper.Map<Bid>(request);
            bid.BidDate = DateTime.UtcNow;

            await _unitOfWork.Bid.CreateAsync(bid);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = "Bid created successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error creating bid: {ex.Message}";
            
        }

        return response;
    }
}
