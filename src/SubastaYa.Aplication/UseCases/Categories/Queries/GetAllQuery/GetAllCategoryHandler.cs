using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Category;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Categories.Queries.GetAllQuery;

internal sealed class GetAllCategoryHandler : IRequestHandler<GetAllCategoryQuery, BaseResponse<IEnumerable<CategoryResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<CategoryResponseDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<CategoryResponseDto>>();

        try
        {
            var categories = await _unitOfWork.Category.GetAllAsync();

            response.IsSuccess = true;
            response.Data = _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
            response.Message = "Categories retrieved successfully.";
        }
        catch(Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error retrieving categories: {ex.Message}";
        }

        return response;
    }
}
