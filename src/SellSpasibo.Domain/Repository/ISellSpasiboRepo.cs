using System;
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
        
        Task AddTransactionHistory(TransactionHistoryEntity transactionHistory);

        IQueryable<PayInfoEntity> GetPayInfosNotPayed();
        Task AddOrUpdatePayInfo(PayInfoEntity payInfo);

        Task SetUserInfoInactive(string number);
        Task<UserInfoEntity> GetUserInfoByPhoneNumber(string number);
        Task AddOrUpdateUserInfo(UserInfoEntity userInfo);

        IQueryable<TinkoffAccountEntity> GetTinkoffAccounts();
        Task<TinkoffAccountEntity> GetTinkoffAccount(string number);
        Task AddOrUpdateTinkoffAccount(TinkoffAccountEntity account);
    }
}