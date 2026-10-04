using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.UseCases.Auctions.Commands.CreateCommand;

internal sealed class CreateAuctionHandler : IRequestHandler<CreateAuctionCommand, BaseResponse<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAuctionHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<BaseResponse<int>> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<int>();

        try
        {
            var auction = _mapper.Map<Auction>(request);

            auction.CurrentPrice = auction.BasePrice;
            
            var now = DateTime.UtcNow;
            if (request.StartDate > now)
            {
                auction.Status = AuctionStatus.SCHEDULED;
            }
            else
            {
                auction.StartDate = now;
                auction.Status = AuctionStatus.ACTIVE;
            }

            await _unitOfWork.Auction.CreateAsync(auction);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Data = auction.Id;
            response.Message = "Auction created successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error creating auction: {ex.Message}";
        }

        return response;
    }
}
