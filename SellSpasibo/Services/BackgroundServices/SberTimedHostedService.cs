using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SellSpasibo.BLL.Interfaces;

namespace SellSpasibo.Services.BackgroundServices
{
    public class SberTimedHostedService : IHostedService, IDisposable
    {
        private int _executionCount = 0;
        private readonly ILogger<SberTimedHostedService> _logger;
        private Timer _timer;
        private const int TimerSeconds = 40;
        private readonly IServiceProvider _services;

        public SberTimedHostedService(ILogger<SberTimedHostedService> logger,
            IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Sber Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                               TimeSpan.FromSeconds(TimerSeconds));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            var count = Interlocked.Increment(ref _executionCount);
            using var scope = _services.CreateScope();
            var serviceSber = scope.ServiceProvider.GetService<ISberSpasibo>();
            await serviceSber.UpdateSession();
            _logger.LogInformation(
                                   "Tinkoff Timed Hosted Service is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Tinkoff Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
