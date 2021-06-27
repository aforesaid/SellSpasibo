using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.CreateNewOrder;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetInfoByUser;
using SellSpasibo.Core.Options;
using SellSpasibo.Core.Services;
using Xunit;

namespace SellSpasibo.UnitTests.Services
{
    public class TinkoffApiClientTest
    {
        private readonly ITinkoffApiClient _tinkoffService;
        public TinkoffApiClientTest()
        {
            var options = Options.Create(new TinkoffOptions
            {
                Account = "5359537080",
                SessionId = "DamjyXCaYA99fj0rlAEoxxLDMVpy9pNY.m1-prod-api35",
                WuId = "8b2bcbf7a44843c985b090f18ece2d84"
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
            var number = "+";
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
            var paymentDetails = new TAPICreateNewOrdersPaymentDetails()
            {
                Pointer = "+",
                MaskedFIO = "",
            };
            var order = new TAPICreateNewOrderRequest()
            {
                Money = 0.01d,
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
            var number = "79997104978";
            string bankMemberId = null;

            TAPIGetInfoByUserPayload info;
            var counter = 0;
            do
            { 
                info = await _tinkoffService.GetInfoByUser(number);
                if (info != null)
                {
                    counter++;
                    var paymentDetails = new TAPICreateNewOrdersPaymentDetails()
                    {
                        Pointer = $"+{number}",
                        MaskedFIO = info.DisplayInfo.First(x => x.Name == "maskedFIO").Value,
                        PointerLinkId = info.PointerLinkId
                    };
                    var order = new TAPICreateNewOrderRequest()
                    {
                        Money = 0.01d,
                        Details = paymentDetails
                    };
                    var response = await _tinkoffService.CreateNewOrder(order);
                    await Task.Delay(10000);
                }

            } while (info != null);
            Assert.NotEqual(0, counter);
        }
    }
}