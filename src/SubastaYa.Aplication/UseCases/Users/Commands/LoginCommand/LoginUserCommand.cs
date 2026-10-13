using MediatR;
using SubastaYa.Aplication.Commons.Bases;

namespace SubastaYa.Aplication.UseCases.Users.Commands.LoginCommand;

public class LoginUserCommand : IRequest<BaseResponse<string>>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
