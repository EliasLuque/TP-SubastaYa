using MediatR;
using SubastaYa.Aplication.UseCases.Auctions.Commands.CloseExpiredCommand;

namespace SubastaYa.Api.Workers;

public class AuctionClosingWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<AuctionClosingWorker> _logger;

    public AuctionClosingWorker(IServiceScopeFactory scopeFactory, ILogger<AuctionClosingWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Auction Closing Worker is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using(var scope = _scopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var response = await mediator.Send(new CloseExpiredAuctionCommand(), stoppingToken);

                    if(response.IsSuccess && response.Data > 0)
                    {
                        _logger.LogInformation($"Worker closed {response.Data} auctions.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while closing expired auctions.");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
