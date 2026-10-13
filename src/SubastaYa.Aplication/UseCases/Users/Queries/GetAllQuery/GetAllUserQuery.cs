using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.User;

namespace SubastaYa.Aplication.UseCases.Users.Queries.GetAllQuery;

public class GetAllUserQuery : IRequest<BaseResponse<IEnumerable<UserResponseDto>>>
{
}
