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

    public UpdateAuctionHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateAuctionCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existingAuction = await _unitOfWork.Auction.GetByIdAsync(request.Id);
            if (existingAuction == null)
            {
                response.IsSuccess = false;
                response.Message = "Auction not found.";
                return response;
            }

            _mapper.Map(request, existingAuction);
            _unitOfWork.Auction.UpdateAsync(existingAuction);
            await _unitOfWork.SaveChangesAsync();

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
