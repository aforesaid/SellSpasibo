using System.Threading.Tasks;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.CreateNewOrder;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBalance;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBankMember;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetInfoByUser;
using SellSpasibo.Core.Models.Authorization.Tinkoff;

namespace SellSpasibo.Core.Interfaces
{
    public interface ITinkoffApiClient
    {
        Task<TinkoffNewSessionInfo> SendSms(string login);

        Task<bool> Authorize(string sessionId, string password, string operationTicket,
            string code);

        Task<bool> UpdateSession(string sessionId);
        Task<TAPIGetInfoByUserPayload> GetInfoByUser(string sessionId, string number);
        Task<TAPIGetBankMemberResponse> GetBankMember(string sessionId);
        Task<TAPICreateNewOrderResponse> CreateNewOrder(string sessionId, TAPICreateNewOrderRequest order);
        Task<TAPIGetBalanceResponse> GetBalance(string sessionId);
    }
}
