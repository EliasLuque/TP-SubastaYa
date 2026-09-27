using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Category;

namespace SubastaYa.Aplication.UseCases.Categories.Queries.GetByIdQuery;

public class GetCategoryByIdQuery : IRequest<BaseResponse<CategoryResponseDto>>
{
    public int Id { get; set; }
}
