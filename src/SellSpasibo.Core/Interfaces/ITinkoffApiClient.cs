using System.Threading.Tasks;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetInfoByUser;
using SellSpasibo.Core.Models.ModelsJson;
using SellSpasibo.Core.Models.ModelsJson.Tinkoff.AnyBanks;
using SellSpasibo.Core.Models.ModelsJson.Tinkoff.Balance;
using SellSpasibo.Core.Models.ModelsJson.Tinkoff.NewOrder;
using SellSpasibo.Core.Models.ModelsJson.Tinkoff.UserByBank;

namespace SellSpasibo.Core.Interfaces
{
    public interface ITinkoffApiClient
    {
        Task<bool> UpdateSession();
        Task<TAPITinkoffPayloadJson> GetInfoByUser(string number);
        Task<TinkoffGetBanks> GetBankMember();
        Task<TAPITinkoffSendOrderJson> CreateNewOrder(TAPIOrder order);
        Task<TAPITinkoffBalanceOrder> GetBalance();
    }
}
