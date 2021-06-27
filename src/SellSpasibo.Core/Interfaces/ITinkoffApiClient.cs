using System.Threading.Tasks;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.CreateNewOrder;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBalance;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBankMember;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetInfoByUser;

namespace SellSpasibo.Core.Interfaces
{
    public interface ITinkoffApiClient
    {
        Task<bool> UpdateSession();
        Task<TAPIGetInfoByUserPayload> GetInfoByUser(string number);
        Task<TAPIGetBankMemberResponse> GetBankMember();
        Task<TAPICreateNewOrderResponse> CreateNewOrder(TAPICreateNewOrderRequest order);
        Task<TAPIGetBalanceResponse> GetBalance();
    }
}
