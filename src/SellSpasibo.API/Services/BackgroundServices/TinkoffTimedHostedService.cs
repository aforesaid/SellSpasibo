using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SellSpasibo.Core.Interfaces;

namespace SellSpasibo.API.Services.BackgroundServices
{
    public class TinkoffTimedHostedService : IHostedService, IDisposable
    {
        private int _executionCount = 0;
        private readonly ILogger<TinkoffTimedHostedService> _logger;
        private Timer _timer;
        private const int TimerSeconds = 120;
        private readonly IServiceProvider _services;
        public TinkoffTimedHostedService(ILogger<TinkoffTimedHostedService> logger,
            IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Tinkoff Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                               TimeSpan.FromSeconds(TimerSeconds));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            var count = Interlocked.Increment(ref _executionCount);
            using var scope = _services.CreateScope();
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
