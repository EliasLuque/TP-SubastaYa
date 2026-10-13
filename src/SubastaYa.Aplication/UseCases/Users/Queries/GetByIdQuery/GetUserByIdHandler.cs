using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.User;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Users.Queries.GetByIdQuery;

internal sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, BaseResponse<UserResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<UserResponseDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserResponseDto>();

        try
        {
            var user = await _unitOfWork.User.GetByIdProjectedAsync<UserResponseDto>(request.Id, _mapper.ConfigurationProvider, cancellationToken);

            if(user == null)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = $"User with ID {request.Id} not found.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = user;
            response.Message = "User retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = null;
            response.Message = $"Error retrieving user by ID: {ex.Message}";
        }

        return response;
    }
}
