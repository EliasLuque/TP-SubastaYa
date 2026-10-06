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
            var category = await _unitOfWork.Category.GetByIdProjectedAsync<CategoryResponseDto>(request.Id, _mapper.ConfigurationProvider, cancellationToken);

            if(category == null)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = "Category not found";
                return response;
            }

            response.IsSuccess = true;
            response.Data = category;
            response.Message = "Category retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = null;
            response.Message = $"Error retrieving category: {ex.Message}";
        }

        return response;
    }
}
