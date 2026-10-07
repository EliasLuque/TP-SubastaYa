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
            var category = await _unitOfWork.Category.GetByIdAsync(request.Id);

            if(request.Name == null && request.IconUrl == null)
            {
                category.
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error updating category: {ex.Message}";
            throw;
        }

        return response;
        
    }
}
