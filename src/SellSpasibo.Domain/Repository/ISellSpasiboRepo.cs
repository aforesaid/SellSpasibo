using System.Linq;
using System.Threading.Tasks;
using SellSpasibo.Domain.Entities;

namespace SellSpasibo.Domain.Repository
{
    public interface ISellSpasiboRepo
    {
        IQueryable<BankEntity> GetBanks();
        Task AddOrUpdateBanks(BankEntity[] banks);

        IQueryable<TransactionEntity> GetTransactions(bool? isPaid = null);
        Task AddOrUpdateTransaction(TransactionEntity transaction);

        Task SetUserInfoInactive(string number);
        Task<UserInfoEntity> GetUserInfoByPhoneNumber(string number);
        Task AddOrUpdateUserInfo(UserInfoEntity userInfo);
    }
}