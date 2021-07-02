
using System;
using System.Threading.Tasks;

namespace SellSpasibo.Core.Interfaces
{
    public interface IPayerTransactionManager
    {
        Task TrySendAllNotPayingTransaction();
        Task AddNotPayingTransaction(string number, double amount, Guid transactionId);

    }
}