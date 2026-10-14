using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Bid;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Bids.Queries.GetByUserIdQuery;

internal sealed class GetBidByUserIdHandler : IRequestHandler<GetBidByUserIdQuery, BaseResponse<IEnumerable<BidResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBidByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<BidResponseDto>>> Handle(GetBidByUserIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<BidResponseDto>>();

        try
        {
            var bids = await _unitOfWork.Bid.GetByUserIdAsync(request.UserId, cancellationToken);

            response.IsSuccess = true;
            response.Data = _mapper.Map<IEnumerable<BidResponseDto>>(bids);
            response.Message = "Bids retrieved successfully";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = null;
            response.Message = $"Error retrieving bids: {ex.Message}";
        }

        return response;
    }
}
