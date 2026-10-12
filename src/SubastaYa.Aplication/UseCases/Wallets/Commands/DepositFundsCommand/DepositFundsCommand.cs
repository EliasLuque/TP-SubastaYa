using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using System.Text.Json.Serialization;

namespace SubastaYa.Aplication.UseCases.Wallets.Commands.DepositFundsCommand;

public class DepositFundsCommand : IRequest<BaseResponse<bool>>
{
    public decimal Amount { get; set; }
    
    [JsonIgnore]
    public int UserId { get; set; }
}
