using System;
using System.Threading.Tasks;

namespace SellSpasibo.Core.Interfaces
{
    interface ITransactionService
    {
        Task<string> CreateNewSberSpasiboOrder(DateTime dateTime);
    }
}
