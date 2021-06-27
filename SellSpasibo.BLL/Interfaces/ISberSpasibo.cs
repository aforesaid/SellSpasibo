using System.Threading.Tasks;
using SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.CheckClient;
using SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.CreateNewOrder;
using SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.GetBalance;
using SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.GetTransactionHistory;

namespace SellSpasibo.BLL.Interfaces
{
    public interface ISberSpasibo
    {
        Task<bool> UpdateSession();
        Task<SAPITransaction[]> GetTransactionHistory();
        Task<SAPICreateNewOrderResponse> CreateNewOrder(double cost, string number);
        Task<SAPICheckClientResponse> CheckClient(string number);
        Task<SAPIGetCurrentBalanceResponse> GetBalance();
    }
}
