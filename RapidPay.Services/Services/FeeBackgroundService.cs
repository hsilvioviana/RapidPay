using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RapidPay.Domain.Interfaces;
using RapidPay.Services.Interfaces;

public class FeeBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private static readonly TimeSpan REFRESH_FREQUENCY = TimeSpan.FromHours(1);

    public FeeBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var feeRepository = scope.ServiceProvider.GetRequiredService<IFeeRepository>();
            var feeService = scope.ServiceProvider.GetRequiredService<IFeeService>();

            var lastFee = await feeRepository.GetLastAsync();
            var elapsed = DateTime.UtcNow - lastFee.CreatedAt.ToUniversalTime();

            if (elapsed >= REFRESH_FREQUENCY)
            {
                await feeService.UpdateFeeAsync();
            }

            var delay = REFRESH_FREQUENCY - elapsed;
            if (delay < TimeSpan.Zero) delay = REFRESH_FREQUENCY;

            await Task.Delay(delay, stoppingToken);
        }
    }
}
