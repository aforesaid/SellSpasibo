using System.Linq;
using SellSpasibo.Domain.Entities;

namespace SellSpasibo.Core.Models.ObserverAccounts
{
    public class OrderAccountInfo
    {
        public OrderAccountInfo(TinkoffObserverAccount account, double money)
        {
            Account = account;
            Money = money;
        }

        public TinkoffObserverAccount Account { get; }
        public double Money { get; }
        public TransactionStatusEnum Status { get; private set; } = TransactionStatusEnum.None;

        public void SetTransactionStatus(TransactionStatusEnum status)
        {
            Status = status;
        }
    }

    public class OrderAccounts
    {
        public OrderAccounts(OrderAccountInfo[] accounts)
        {
            Accounts = accounts;
            Money = Accounts.Sum(x => x.Money);
        }

        public OrderAccountInfo[] Accounts { get; }

        public double Money { get; }
    }
}