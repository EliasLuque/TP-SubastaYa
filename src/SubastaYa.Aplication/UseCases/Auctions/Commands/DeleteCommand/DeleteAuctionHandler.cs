using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.DeleteCommand;

internal class DeleteAuctionHandler : IRequestHandler<DeleteAuctionCommand, BaseResponse<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuctionHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<int>> Handle(DeleteAuctionCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<int>();

        try
        {
            var auction = await _unitOfWork.Auction.GetByIdAsync(request.Id);

            if (auction == null)
            {
                response.IsSuccess = false;
                response.Message = "Auction not found";
                return response;
            }

            if (auction.SellerId != request.SellerId)
            {
                response.IsSuccess = false;
                response.Message = "You are not the owner of this auction";
                return response;
            }

            if(auction.Status != AuctionStatus.SCHEDULED)
            {
                response.IsSuccess = false;
                response.Message = "Only scheduled auctions can be deleted";
                return response;
            }

            auction.State = EntityState.INACTIVE;

            _unitOfWork.Auction.Update(auction);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Data = auction.Id;
            response.Message = "Auction deleted successfully";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error deleting auction: {ex.Message}";
        }

        return response;
    }
}
