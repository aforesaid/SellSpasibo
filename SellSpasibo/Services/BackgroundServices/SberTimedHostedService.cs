﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SellSpasibo.Services.BackgroundServices
{
    public class SberTimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<SberTimedHostedService> _logger;
        private Timer _timer;
        private const int TimerSeconds = 120;
        public SberTimedHostedService(ILogger<SberTimedHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Sber Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                               TimeSpan.FromSeconds(TimerSeconds));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);
            //TODO: добавить обновление токенов сбер

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