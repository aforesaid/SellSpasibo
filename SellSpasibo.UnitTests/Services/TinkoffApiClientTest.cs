using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SellSpasibo.BLL.Interfaces;
using SellSpasibo.BLL.Models.ModelsJson;
using SellSpasibo.BLL.Models.ModelsJson.Tinkoff.UserByBank;
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
            var number = "";
            var actual = await _tinkoffService.GetInfoByUser(number);
            Assert.NotNull(actual);
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
                Money = Math.Truncate(0.01m),
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

        [Fact]
        public async Task TestCountRequestLimit_Expected_No()
        {
            var number = "";
            string bankMemberId = null;
            
           

            TinkoffPayloadJson info;
            var counter = 0;
            do
            { 
                info = await _tinkoffService.GetInfoByUser(number);
                if (info != null)
                {
                    counter++;
                    var paymentDetails = new PaymentDetails()
                    {
                        Pointer = number,
                        MaskedFIO = info.DisplayInfo.First(x => x.Name == "value").Value,
                        PointerLinkId = info.PointerLinkId
                    };
                    var order = new Order()
                    {
                        Money = Math.Truncate(0.01m),
                        Details = paymentDetails
                    };
                    await _tinkoffService.CreateNewOrder(order);
                    await Task.Delay(1000);
                }

            } while (info != null);
            Assert.NotEqual(0, counter);
        }
    }
}