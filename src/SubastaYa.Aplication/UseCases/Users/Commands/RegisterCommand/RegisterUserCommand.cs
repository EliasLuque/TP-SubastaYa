using MediatR;
using SubastaYa.Aplication.Commons.Bases;

namespace SubastaYa.Aplication.UseCases.Users.Commands.RegisterCommand;

public class RegisterUserCommand :  IRequest<BaseResponse<int>>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
