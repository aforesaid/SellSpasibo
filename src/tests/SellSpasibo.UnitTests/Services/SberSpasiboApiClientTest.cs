using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Core.Options;
using SellSpasibo.Core.Services;
using Xunit;

namespace SellSpasibo.API.UnitTests.Services
{
    public class SberSpasiboApiClientTest
    {
        private readonly ISberSpasiboApiClient _sberSpasiboService;
        public SberSpasiboApiClientTest()
        {
            var options = Options.Create(new SberOptions()
            {
                AuthToken = "",
                RefreshToken = ""
            });
            var moqLogging = new Mock<ILogger<SberSpasiboApiClient>>();
            _sberSpasiboService = new SberSpasiboApiClient(moqLogging.Object, options);
        }

        [Fact]
        public async Task UpdateSession_Expected_True()
        {
            var actual = await _sberSpasiboService.UpdateSession();
            Assert.True(actual);
        }

        [Fact]
        public async Task GetTransactionHistory()
        {
            var result = await _sberSpasiboService.GetTransactionHistory();
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task CreateNewOrder()
        {
            double cost = 8;
            var number = "+";
            var actual = await _sberSpasiboService.CreateNewOrder(cost, number);
            Assert.NotNull(actual?.Data);
            Assert.True(actual.Data.Status);
        }

        [Fact]
        public async Task GetBalance_Expected_Value()
        {
            var actual = await _sberSpasiboService.GetBalance();
            var balance = actual.Data.LoyaltySystem.Balance;
            Assert.NotEqual(0, balance);
        }
        [Fact]
        public async Task CheckClient_Expected_Not_Zero()
        {
            var phone = "+";
            var actual = await _sberSpasiboService.CheckClient(phone);
            Assert.NotNull(actual.Data);
        }

        [Fact]
        public async Task CheckTimeInLive_Expected_More_Than_One_Hours()
        {
            var watch = new Stopwatch();
            watch.Start();
            bool actual;
            do
            {
                actual = await _sberSpasiboService.UpdateSession();
                if (watch.Elapsed > TimeSpan.FromHours(1))
                    break;
                await Task.Delay(10000);
            } while (actual);
            watch.Stop();
            Assert.True(watch.Elapsed > TimeSpan.FromHours(1));
        }
        
    }
}