using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Auction;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Auctions.Queries.GetByIdQuery;

internal sealed class GetByIdHandler : IRequestHandler<GetByIdQuery, BaseResponse<AuctionResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<AuctionResponseDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<AuctionResponseDto>();

        try
        {
            var auction = await _unitOfWork.Auction.GetByIdProjectedAsync<AuctionResponseDto>(request.Id, _mapper.ConfigurationProvider);

            if(auction == null)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = "Auction not found.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = auction;
            response.Message = "Auction retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = null;
            response.Message = $"Error: {ex.Message}";
        }

        return response;
    }
}
