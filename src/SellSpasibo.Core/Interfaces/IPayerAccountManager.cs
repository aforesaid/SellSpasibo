
using System;
using System.Threading.Tasks;

namespace SellSpasibo.Core.Interfaces
{
    public interface IPayerAccountManager
    {
        Task TrySendAllNotPayingTransaction();
        Task AddNotPayingTransaction(string number, double amount, Guid transactionId);

    }
}