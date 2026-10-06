using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Category;

namespace SubastaYa.Aplication.UseCases.Categories.Queries.GetAllQuery;

public class GetAllCategoryQuery : IRequest<BaseResponse<IEnumerable<CategoryResponseDto>>>
{
}
