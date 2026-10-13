using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Users.Commands.DeleteCommand;

internal sealed class DeleteUserHandler : IRequestHandler<DeleteUserCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
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

            var wallet = await _unitOfWork.Wallet.GetByUserIdAsync(request.Id, cancellationToken);
            if(wallet != null)
            {
                if(wallet.HeldBalance > 0)
                {
                    response.IsSuccess = false;
                    response.Data = false;
                    response.Message = "Cannot delete user with retained balance in active auctions.";
                }

                wallet.State = Domain.Entities.EntityState.INACTIVE;
                _unitOfWork.Wallet.Update(wallet);
            }

            user.State = Domain.Entities.EntityState.INACTIVE;
            _unitOfWork.User.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Data = true;
            response.Message = "User deleted successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = false;
            response.Message = $"Error deleting user: {ex.Message}";
        }

        return response;
    }
}
