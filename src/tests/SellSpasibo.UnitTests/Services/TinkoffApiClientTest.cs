using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.CreateNewOrder;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetInfoByUser;
using SellSpasibo.Core.Services;
using Xunit;

namespace SellSpasibo.UnitTests.Services
{
    public class TinkoffApiClientTest
    {
        private readonly ITinkoffApiClient _tinkoffService;
        private readonly string _sessionId;
        private readonly string _account;
        public TinkoffApiClientTest()
        {
            _sessionId = "";
            _account = "";
            var moqLogging = new Mock<ILogger<TinkoffApiClient>>();
            
            _tinkoffService = new TinkoffApiClient(moqLogging.Object);
        }
        [Fact]
        public async Task UpdateSession_Expected_True()
        {
            var actual = await _tinkoffService.UpdateSession(_sessionId);
            Assert.True(actual);
        }

        [Fact]
        public async Task GetInfoByUser_Expected_UserParams()
        {
            var number = "+";
            var actual = await _tinkoffService.GetInfoByUser(_sessionId, number);
            Assert.NotNull(actual);
        }

        [Fact]
        public async Task GetBankMember_Expected_BankMember()
        {
            var actual = await _tinkoffService.GetBankMember(_sessionId);
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
                Details = paymentDetails,
                Account = _account
            };     
            var actual = await _tinkoffService.CreateNewOrder(_sessionId, order);
           
            Assert.NotNull(actual);
            Assert.NotNull(actual.Payload);
        }
        [Fact]
        public async Task GetBalance_Expected_TinkoffBalanceOrder()
        {
            var actual = await _tinkoffService.GetBalance(_sessionId);
           
            Assert.NotNull(actual);
            Assert.NotNull(actual.Payload);
            Assert.NotNull(actual.Payload.Payload);
        }

        [Fact]
        public async Task TestCountRequestLimit_Expected_No()
        {
            var number = "";
            string bankMemberId = null;

            TAPIGetInfoByUserPayload info;
            var counter = 0;
            do
            { 
                info = await _tinkoffService.GetInfoByUser(_sessionId, number);
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
                    var response = await _tinkoffService.CreateNewOrder(_sessionId, order);
                    await Task.Delay(10000);
                }

            } while (info != null);
            Assert.NotEqual(0, counter);
        }
    }
}