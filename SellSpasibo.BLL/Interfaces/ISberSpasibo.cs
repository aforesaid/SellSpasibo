using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Balance;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.CheckClient;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.History;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.NewOrder;
using System.Threading.Tasks;

namespace SellSpasibo.BLL.Interfaces
{
    public interface ISberSpasibo
    {
        Task<bool> UpdateSession();
        Task<SberSpasiboGetHistoryJson> GetTransactionHistory();
        Task<SberSpasiboNewOrderJson> CreateNewOrder(string cost, string number);
        Task<SberSpasiboCheckClientJson> CheckClient(string number);
        Task<SberSpasiboGetCurrentBalanceJson> GetBalance();
    }
}
