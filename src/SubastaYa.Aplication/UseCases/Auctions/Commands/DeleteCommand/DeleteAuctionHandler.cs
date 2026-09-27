using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.DeleteCommand;

internal sealed class DeleteAuctionHandler : IRequestHandler<DeleteAuctionCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuctionHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteAuctionCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        
        try
        {
            var existAuction = await _unitOfWork.Auction.GetByIdAsync(request.Id);
            
            if(existAuction == null)
                throw new KeyNotFoundException($"Auction with Id {request.Id} not found.");
            
            await _unitOfWork.Auction.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Auction deleted successfully."; 

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error deleting auction: {ex.Message}";
        }

        return response;
    }
}
