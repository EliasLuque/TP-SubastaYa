using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using System.Text.Json.Serialization;

namespace SubastaYa.Aplication.UseCases.Users.Commands.UpdateCommand;

public class UpdateUserCommand : IRequest<BaseResponse<bool>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}
