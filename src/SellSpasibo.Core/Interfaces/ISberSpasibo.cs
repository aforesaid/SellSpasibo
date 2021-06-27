using System.Threading.Tasks;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.CheckClient;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.CreateNewOrder;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.GetBalance;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.GetTransactionHistory;

namespace SellSpasibo.Core.Interfaces
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
