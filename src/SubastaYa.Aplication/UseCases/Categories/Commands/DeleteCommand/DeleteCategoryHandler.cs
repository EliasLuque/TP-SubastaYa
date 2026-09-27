using AutoMapper.Configuration.Annotations;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;

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
            var existingCategory = _unitOfWork.Category.GetByIdAsync(request.Id);

            if(existingCategory == null)
            {
                response.IsSuccess = false;
                response.Message = "Category not found.";
                return response;
            }

            await _unitOfWork.Category.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
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
