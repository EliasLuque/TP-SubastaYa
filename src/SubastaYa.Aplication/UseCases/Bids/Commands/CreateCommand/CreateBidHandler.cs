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
                response.Data = -1;
                response.Message = "Auction not found.";
                return response;
            }

            if (auction.Status != AuctionStatus.ACTIVE)
            {
                response.IsSuccess = false;
                response.Data = -1;
                response.Message = "Only active auctions can have bids.";
                return response;
            }

            if(auction.SellerId == request.UserId)
            {
                response.IsSuccess = false;
                response.Data = -1;
                response.Message = "The seller cannot bid.";
                return response;
            }

            var currentHighestPrice = auction.CurrentPrice > 0 ? auction.CurrentPrice : auction.BasePrice;
            var minimumAccepted = auction.CurrentPrice > 0 ? currentHighestPrice + auction.MinimumIncrement : auction.BasePrice;

            if(request.Amount < minimumAccepted)
            {
                response.IsSuccess = false;
                response.Data = -1;
                response.Message = $"The bid amount must be at least {minimumAccepted}.";
                return response;
            }

            var bidderWallet = await _unitOfWork.Wallet.GetByUserIdAsync(request.UserId, cancellationToken);
            if(bidderWallet == null || bidderWallet.AvailableBalance < request.Amount)
            {
                response.IsSuccess = false;
                response.Data = -1;
                response.Message = "Insufficient available funds to place this bid.";
                return response;
            }

            //******************************
            // Liberación del saldo retenido
            //******************************
            var previousHighestBid = await _unitOfWork.Bid.GetHighestBidByAuctionIdAsync(request.AuctionId, cancellationToken);
            if(previousHighestBid != null)
            {
                var previousBidderWallet = await _unitOfWork.Wallet.GetByUserIdAsync(previousHighestBid.UserId, cancellationToken);

                previousBidderWallet.HeldBalance -= previousHighestBid.Amount;
                previousBidderWallet.AvailableBalance += previousHighestBid.Amount;
                _unitOfWork.Wallet.Update(previousBidderWallet);

                await _unitOfWork.TransactionLedger.CreateAsync(new Domain.Entities.TransactionLedger
                {
                    WalletId = previousBidderWallet.Id,
                    Type = TransactionType.RELEASE,
                    Amount = previousHighestBid.Amount,
                    Date = DateTime.UtcNow,
                    AuctionId = auction.Id
                });
            }

            //********************
            // Retención del saldo
            //********************
            bidderWallet.AvailableBalance -= request.Amount;
            bidderWallet.HeldBalance += request.Amount;
            _unitOfWork.Wallet.Update(bidderWallet);
            await _unitOfWork.TransactionLedger.CreateAsync(new Domain.Entities.TransactionLedger
            {
                WalletId = bidderWallet.Id,
                Type = TransactionType.RETENTION,
                Amount = request.Amount,
                Date = DateTime.UtcNow,
                AuctionId = auction.Id
            });

            //*******************
            // Regla Anti-Sniping
            //*******************
            var timeRemaining = auction.EndDate - DateTime.UtcNow;
            if(timeRemaining.TotalMinutes <= 5)
            {
                auction.EndDate = auction.EndDate.AddMinutes(5);
            }

            auction.CurrentPrice = request.Amount;
            _unitOfWork.Auction.Update(auction);

            var bid = _mapper.Map<Bid>(request);
            bid.BidDate = DateTime.UtcNow;
            await _unitOfWork.Bid.CreateAsync(bid);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Data = bid.Id;
            response.Message = "Bid placed successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = -1;
            response.Message = $"Error placing bid: {ex.Message}";
            
        }

        return response;
    }
}