using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Authentication;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Users.Commands.LoginCommand;

internal sealed class LoginUserHandler : IRequestHandler<LoginUserCommand, BaseResponse<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<BaseResponse<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<string>();

        try
        {
            var user = await _unitOfWork.User.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = "Invalid email or password.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = _jwtTokenGenerator.GenerateToken(user);
            response.Message = "User logged in successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error logging in: {ex.Message}";
        }

        return response;
    }
}
