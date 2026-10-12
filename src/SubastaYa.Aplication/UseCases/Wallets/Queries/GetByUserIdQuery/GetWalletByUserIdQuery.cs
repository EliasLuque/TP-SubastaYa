using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Dtos.Wallet;
using System.Text.Json.Serialization;

namespace SubastaYa.Aplication.UseCases.Wallets.Queries.GetByUserIdQuery;

public class GetWalletByUserIdQuery : IRequest<BaseResponse<WalletResponseDto>>
{
    [JsonIgnore]
    public int UserId { get; set; }
}
