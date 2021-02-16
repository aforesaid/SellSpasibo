using System;
using System.Threading.Tasks;

namespace SellSpasibo.BLL.Interfaces
{
    interface ITransactionService
    {
        Task<string> CreateNewSberSpasiboOrder(DateTime dateTime);
    }
}
