using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.UseCases.Categories.Commands.CreateCommand;

internal sealed class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, BaseResponse<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<int>();

        try
        {
            var category = _mapper.Map<Category>(request);

            if(category.IconUrl == null)
            {
                category.IconUrl = "poner aca la url de icono por defecto?";
            }

            await _unitOfWork.Category.CreateAsync(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Data = category.Id;
            response.Message = $"Created category with id: {category.Id} successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = -1;
            response.Message = $"Error creating category: {ex.Message}";
        }

        return response;
    }
}
