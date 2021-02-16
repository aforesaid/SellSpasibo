using SellSpasibo.BLL.Models;
using SellSpasibo.BLL.Models.ModelsJson.Tinkoff.AnyBanks;
using SellSpasibo.BLL.Models.ModelsJson.Tinkoff.Balance;
using SellSpasibo.BLL.Models.ModelsJson.Tinkoff.NewOrder;
using SellSpasibo.BLL.Models.ModelsJson.Tinkoff.UserByBank;
using System.Threading.Tasks;
using SellSpasibo.BLL.Models.ModelsJson;

namespace SellSpasibo.BLL.Interfaces
{
    public interface ITinkoff
    {
        Task<bool> UpdateSession();
        Task<TinkoffCheckUserParams> GetInfoByUser(string number, string bankMemberId);
        Task<TinkoffGetBanks> GetBankMember();
        Task<TinkoffSendOrderJson> CreateNewOrder(Order order);
        Task<TinkoffBalanceOrder> GetBalance();
    }
}
