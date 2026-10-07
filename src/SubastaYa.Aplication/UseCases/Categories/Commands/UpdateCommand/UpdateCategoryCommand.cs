using MediatR;
using SubastaYa.Aplication.Commons.Bases;

namespace SubastaYa.Aplication.UseCases.Categories.Commands.UpdateCommand;

public class UpdateCategoryCommand : IRequest<BaseResponse<bool>>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? IconUrl { get; set; }
}
