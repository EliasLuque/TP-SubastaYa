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
            var categories = await _unitOfWork.Category.GetAllProjectedAsync<CategoryResponseDto>(_mapper.ConfigurationProvider, cancellationToken);

            response.IsSuccess = true;
            response.Data = categories;
            response.Message = "Categories retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = null;
            response.Message = $"Error retrieving categories: {ex.Message}";
        }

        return response;
    }
}
