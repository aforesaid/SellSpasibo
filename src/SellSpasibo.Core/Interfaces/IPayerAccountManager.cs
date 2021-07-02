using System.Threading.Tasks;
using SellSpasibo.Domain.Entities;

namespace SellSpasibo.Core.Interfaces
{
    public interface IPayerAccountManager
    {
        Task AddTinkoffAccount(string phone, string password, string accountId);
        Task<TinkoffAccountEntity[]> GetAccounts();
        Task<TinkoffAccountEntity> GetAccountByPhone(string phone);
    }
}