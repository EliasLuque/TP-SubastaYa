using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.UpdateCommand;

internal sealed class UpdateAuctionHandler : IRequestHandler<UpdateAuctionCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAuctionHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateAuctionCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var auction = await _unitOfWork.Auction.GetByIdAsync(request.Id);

            if(auction == null)
            {
                response.IsSuccess = false;
                response.Message = "Auction not found.";
                return response;
            }

            if(auction.SellerId != request.SellerId)
            {
                response.IsSuccess = false;
                response.Message = "You are not the owner of this auction.";
                return response;
            }

            if(auction.Status != AuctionStatus.SCHEDULED)
            {
                response.IsSuccess = false;
                response.Message = "Only scheduled auctions can be updated.";
                return response;
            }

            _mapper.Map(request, auction);

            _unitOfWork.Auction.Update(auction);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = "Auction updated successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error updating auction: {ex.Message}";
        }

        return response;
    }
}
