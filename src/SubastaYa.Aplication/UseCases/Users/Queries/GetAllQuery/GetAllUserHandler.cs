using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.User;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Users.Queries.GetAllQuery;

internal sealed class GetAllUserHandler : IRequestHandler<GetAllUserQuery, BaseResponse<IEnumerable<UserResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<UserResponseDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<UserResponseDto>>();

        try
        {
            var users = await _unitOfWork.User.GetAllProjectedAsync<UserResponseDto>(_mapper.ConfigurationProvider, cancellationToken);

            response.IsSuccess = true;
            response.Data = users;
            response.Message = "Users retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = null;
            response.Message = $"Error retrieving users: {ex.Message}";
        }

        return response;
    }
}
