using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Users.Commands.UpdateCommand;

internal class UpdateUserHandler : IRequestHandler<UpdateUserCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var user = await _unitOfWork.User.GetByIdAsync(request.Id);

            if(user == null)
            {
                response.IsSuccess = false;
                response.Data = false;
                response.Message = "User not found";
                return response;
            }

            if(!string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase))
            {
                var existingUser = await _unitOfWork.User.GetUserByEmailAsync(request.Email, cancellationToken);
                if(existingUser != null && existingUser.Id != user.Id)
                {
                    response.IsSuccess = false;
                    response.Data = false;
                    response.Message = "The email address is already in use by another account.";
                    return response;
                }
            }

            _mapper.Map(request, user);

            _unitOfWork.User.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Data = true;
            response.Message = "User updated successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = false;
            response.Message = $"Error updating user: {ex.Message}";
        }

        return response;
    }
}
