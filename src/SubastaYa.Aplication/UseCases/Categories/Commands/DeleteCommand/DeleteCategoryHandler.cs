using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;
using System.Runtime.CompilerServices;

namespace SubastaYa.Aplication.UseCases.Categories.Commands.DeleteCommand;

internal sealed class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var category = await _unitOfWork.Category.GetByIdAsync(request.Id);
            
            if(category == null)
            {
                response.IsSuccess = false;
                response.Message = "Category not found";
                return response;
            }

            category.State = Domain.Entities.EntityState.INACTIVE;

            _unitOfWork.Category.Update(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Data = true;
            response.Message = "Category deleted successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error deleting category: {ex.Message}";
        }

        return response;
    }
}
