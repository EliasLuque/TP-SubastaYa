using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.CloseExpiredCommand;

internal sealed class CloseExpiredAuctionHandler : IRequestHandler<CloseExpiredAuctionCommand, BaseResponse<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CloseExpiredAuctionHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<int>> Handle(CloseExpiredAuctionCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<int>();
        int processedCount = 0;

        try
        {
            var expiredAuctions = await _unitOfWork.Auction.GetExpiredActiveAuctionAsync(cancellationToken);

            foreach(var auction in expiredAuctions)
            {
                var winningBid = await _unitOfWork.Bid.GetHighestBidByAuctionIdAsync(auction.Id, cancellationToken);

                if (winningBid == null)
                {
                    auction.Status = AuctionStatus.UNSOLD;
                }
                else
                {
                    auction.Status = AuctionStatus.FINISHED;

                    var buyerWallet = await _unitOfWork.Wallet.GetByUserIdAsync(winningBid.UserId, cancellationToken);
                    var sellerWallet = await _unitOfWork.Wallet.GetByUserIdAsync(auction.SellerId, cancellationToken);

                    //**********************
                    // Liquidación de fondos
                    //**********************
                    buyerWallet.HeldBalance -= winningBid.Amount;

                    sellerWallet.TotalBalance += winningBid.Amount;
                    sellerWallet.AvailableBalance += winningBid.Amount;

                    //*******************************
                    // Registro en Transaction Ledger
                    //*******************************
                    await _unitOfWork.TransactionLedger.CreateAsync(new Domain.Entities.TransactionLedger
                    {
                        WalletId = buyerWallet.Id,
                        Type = TransactionType.PAYMENT,
                        Amount = winningBid.Amount,
                        Date = DateTime.UtcNow,
                        AuctionId = auction.Id
                    });

                    await _unitOfWork.TransactionLedger.CreateAsync(new Domain.Entities.TransactionLedger
                    {
                        WalletId = sellerWallet.Id,
                        Type = TransactionType.COLLECTION,
                        Amount = winningBid.Amount,
                        Date = DateTime.UtcNow,
                        AuctionId = auction.Id
                    });

                    _unitOfWork.Wallet.Update(buyerWallet);
                    _unitOfWork.Wallet.Update(sellerWallet);
                }

                _unitOfWork.Auction.Update(auction);
                processedCount++;
            }

            if(processedCount > 0)
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            response.IsSuccess = true;
            response.Data = processedCount;
            response.Message = $"Successfully processed {processedCount} expired auctions.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = processedCount;
            response.Message = $"Error closing auctions: {ex.Message}";
        }

        return response;
    }
}
