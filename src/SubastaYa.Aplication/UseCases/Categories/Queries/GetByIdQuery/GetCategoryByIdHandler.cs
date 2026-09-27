using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Category;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Categories.Queries.GetByIdQuery;

internal sealed class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, BaseResponse<CategoryResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<CategoryResponseDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<CategoryResponseDto>();

        try
        {
            var existCategory = await _unitOfWork.Category.GetByIdAsync(request.Id);

            if(existCategory == null)
            {
                response.IsSuccess = false;
                response.Message = "Category not found.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<CategoryResponseDto>(existCategory);
            response.Message = "Category retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error retrieving category: {ex.Message}";
        }

        return response;
    }
}
