using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Bid;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Bids.Queries.GetAllQuery;

internal sealed class GetAllBidHandler : IRequestHandler<GetAllBidQuery, BaseResponse<IEnumerable<BidResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllBidHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<BidResponseDto>>> Handle(GetAllBidQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<BidResponseDto>>();

        try
        {
            var bids = await _unitOfWork.Bid.GetAllProjectedAsync<BidResponseDto>(_mapper.ConfigurationProvider, cancellationToken);

            response.IsSuccess = true;
            response.Data = bids;
            response.Message = $"Bids retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = true;
            response.Data = null;
            response.Message = $"Error retrieving Bids: {ex.Message}";
        }

        return response;
    }
}
