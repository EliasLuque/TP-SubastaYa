using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;

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
            var auction = _mapper.Map<Domain.Entities.Auction>(request);
            auction.Id = request.Id;
            _unitOfWork.Auction.UpdateAsync(auction);
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
