using MediatR;
using SubastaYa.Aplication.Commons.Bases;
using SubastaYa.Aplication.Interface.Services;

namespace SubastaYa.Aplication.UseCases.Wallets.Commands.DepositFundsCommand;

internal sealed class DepositFundsHandler : IRequestHandler<DepositFundsCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DepositFundsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DepositFundsCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var wallet = await _unitOfWork.Wallet.GetByUserIdAsync(request.UserId);

            if (wallet == null)
            {
                response.IsSuccess = false;
                response.Message = "Wallet not found for the specified user.";
                return response;
            }

            wallet.AvailableBalance += request.Amount;
            wallet.TotalBalance += request.Amount;

            var ledgerEntry = new Domain.Entities.TransactionLedger
            {
                WalletId = wallet.Id,
                Amount = request.Amount,
                Type = Domain.Entities.TransactionType.DEPOSIT,
                Date = DateTime.UtcNow,
                AuctionId = null
            };

            _unitOfWork.Wallet.Update(wallet);
            await _unitOfWork.TransactionLedger.CreateAsync(ledgerEntry);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = "Funds deposited successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error depositing funds: {ex.Message}";
            throw;
        }

        return response;
    }
}
