using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Categories.Commands.UpdateCommand;

internal sealed class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existCategory = await _unitOfWork.Category.GetByIdAsync(request.Id);
            if (existCategory == null)
            {
                response.IsSuccess = false;
                response.Message = "Category not found.";
                return response;
            }

            _mapper.Map(request, existCategory);
            _unitOfWork.Category.UpdateAsync(existCategory);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Category updated successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error updating category: {ex.Message}";
        }

        return response;
    }
}
