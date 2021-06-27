using System.Threading.Tasks;
using SellSpasibo.Core.Models.ModelsJson;
using SellSpasibo.Core.Models.ModelsJson.Tinkoff.AnyBanks;
using SellSpasibo.Core.Models.ModelsJson.Tinkoff.Balance;
using SellSpasibo.Core.Models.ModelsJson.Tinkoff.NewOrder;
using SellSpasibo.Core.Models.ModelsJson.Tinkoff.UserByBank;

namespace SellSpasibo.Core.Interfaces
{
    public interface ITinkoff
    {
        Task<bool> UpdateSession();
        Task<TinkoffPayloadJson> GetInfoByUser(string number);
        Task<TinkoffGetBanks> GetBankMember();
        Task<TinkoffSendOrderJson> CreateNewOrder(Order order);
        Task<TinkoffBalanceOrder> GetBalance();
    }
}
