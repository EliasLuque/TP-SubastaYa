using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.UseCases.Categories.Commands.CreateCommand;

internal sealed class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var category = _mapper.Map<Category>(request);
            await _unitOfWork.Category.CreateAsync(category);

            response.IsSuccess = true;
            response.Message = "Category created successfully.";

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error creating category: {ex.Message}";
        }

        return response;
    }
}
