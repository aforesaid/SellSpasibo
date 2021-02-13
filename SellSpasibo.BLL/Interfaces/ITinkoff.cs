using System.Threading.Tasks;
using SellSpasibo.BLL.Models;

namespace SellSpasibo.BLL.Interfaces
{
    public interface ITinkoff
    {
        Task<bool> UpdateSession();
        Task<string> GetInfoByUser(string number, string bankMemberId);
        Task<string> GetBankMember();
        Task<string> CreateNewOrder(Order order);
    }
}
