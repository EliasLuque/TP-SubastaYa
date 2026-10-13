using AutoMapper;
using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.UseCases.Users.Commands.RegisterCommand;

internal sealed class RegisterUserHandler : IRequestHandler<RegisterUserCommand, BaseResponse<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<int>();

        try
        {
            var emailExist = await _unitOfWork.User.ExistsByEmailAsync(request.Email, cancellationToken);

            if (emailExist)
            {
                response.IsSuccess = false;
                response.Data = -1;
                response.Message = "The email address is already registered";
                return response;
            }

            var user = _mapper.Map<User>(request);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.Wallet = new Wallet
            {
                TotalBalance = 0m,
                AvailableBalance = 0m,
                HeldBalance = 0m,
                Version = 1
            };

            await _unitOfWork.User.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Data = user.Id;
            response.Message = "User registered successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Data = -1;
            response.Message = $"Error registering user: {ex.Message}";
        }

        return response;
    }
}
