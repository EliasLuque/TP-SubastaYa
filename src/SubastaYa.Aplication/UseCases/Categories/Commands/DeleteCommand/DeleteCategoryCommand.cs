using MediatR;
using SubastaYa.Aplication.Commons.Bases;

namespace SubastaYa.Aplication.UseCases.Categories.Commands.DeleteCommand;

public class DeleteCategoryCommand : IRequest<BaseResponse<bool>>
{
    public int Id { get; set; }
}
