using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.User;
using System.Text.Json.Serialization;

namespace SubastaYa.Aplication.UseCases.Users.Queries.GetByIdQuery;

public class GetUserByIdQuery : IRequest<BaseResponse<UserResponseDto>>
{
    [JsonIgnore]
    public int Id { get; set; }
}
