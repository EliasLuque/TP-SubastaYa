using MediatR;
using SubastaYa.Aplication.Commons.Bases;

namespace SubastaYa.Aplication.UseCases.Categories.Commands.CreateCommand;

public class CreateCategoryCommand : IRequest<BaseResponse<int>>
{
    public string Name { get; set; } = null!;
    public string? IconUrl { get; set; }
}
