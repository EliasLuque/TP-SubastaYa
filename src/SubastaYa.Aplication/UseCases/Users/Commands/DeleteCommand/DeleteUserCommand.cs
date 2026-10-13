using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using System.Text.Json.Serialization;

namespace SubastaYa.Aplication.UseCases.Users.Commands.DeleteCommand;

public class DeleteUserCommand : IRequest<BaseResponse<bool>>
{
    [JsonIgnore]
    public int Id { get; set; }
}
