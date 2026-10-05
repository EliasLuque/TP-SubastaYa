using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Bid;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Bids.Queries.GeyByIdQuery;

internal sealed class GetBidByIdHandler : IRequestHandler<GetBidByIdQuery, BaseResponse<BidResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetBidByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<BidResponseDto>> Handle(GetBidByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<BidResponseDto>();

        try
        {
            var bid = await _unitOfWork.Bid.GetByIdProjectedAsync<BidResponseDto>(request.Id, _mapper.ConfigurationProvider, cancellationToken);

            if (bid == null)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = $"Bid not found";
                return response;
            }

            response.IsSuccess = true;
            response.Data = bid;
            response.Message = "Bid retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = true;
            response.Data = null;
            response.Message = $"Error: {ex.Message}";
        }

        return response;
    }
}
