using System.Threading.Tasks;
using SellSpasibo.Core.Models.ObserverAccounts;

namespace SellSpasibo.Core.Interfaces
{
    public interface IAccountObserver
    {
        Task UpdateAccountsInfo();
        OrderAccounts SelectAccountsForTransaction(double transactionMoney);
        void UnlockOrRemoveMoneyFromAccounts(OrderAccounts orderAccounts);
        bool AddAccount(TinkoffObserverAccount account);
    }
}