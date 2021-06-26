using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SellSpasibo.BLL.Interfaces;
using SellSpasibo.BLL.Models.ModelsJson;
using SellSpasibo.BLL.Options;
using SellSpasibo.BLL.Services;
using Xunit;

namespace SellSpasibo.UnitTests.Services
{
    public class TinkoffApiClientTest
    {
        private readonly ITinkoff _tinkoffService;
        public TinkoffApiClientTest()
        {
            var options = Options.Create(new TinkoffOptions
            {
                Account = "",
                SessionId = "",
                WuId = ""
            });
            var moqLogging = new Mock<ILogger<TinkoffApiClient>>();
            _tinkoffService = new TinkoffApiClient(moqLogging.Object, options);
        }
        [Fact]
        public async Task UpdateSession_Expected_True()
        {
            var actual = await _tinkoffService.UpdateSession();
            Assert.True(actual);
        }

        [Fact]
        public async Task GetInfoByUser_Expected_UserParams()
        {
            var number = "+777777777";
            string bankMemberId = null;
            var actual = await _tinkoffService.GetInfoByUser(number, bankMemberId);
            Assert.NotNull(actual);
            Assert.NotNull(actual.Payload);
        }

        [Fact]
        public async Task GetBankMember_Expected_BankMember()
        {
            var actual = await _tinkoffService.GetBankMember();
            Assert.NotNull(actual);
            Assert.NotEmpty(actual.Payload);
        }

        [Fact]
        public async Task CreateNewoOrder_Expected_TinkoffSendOrder()
        {
            var paymentDetails = new PaymentDetails()
            {
                Pointer = "",
                MaskedFIO = "",
            };
            var order = new Order()
            {
                Money = Math.Truncate(10m),
                Details = paymentDetails
            };     
            var actual = await _tinkoffService.CreateNewOrder(order);
           
            Assert.NotNull(actual);
            Assert.NotNull(actual.Payload);
        }
        [Fact]
        public async Task GetBalance_Expected_TinkoffBalanceOrder()
        {
            var actual = await _tinkoffService.GetBalance();
           
            Assert.NotNull(actual);
            Assert.NotNull(actual.Payload);
            Assert.NotNull(actual.Payload.Payload);
        }
    }
}