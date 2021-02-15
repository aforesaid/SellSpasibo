using System.Threading.Tasks;

namespace SellSpasibo.BLL.Interfaces
{
    public interface ISberSpasibo
    {
        Task<bool> UpdateSession();
        Task<string> GetTransactionHistory();
        Task<string> CreateNewOrder(string cost, string number);
    }
}
