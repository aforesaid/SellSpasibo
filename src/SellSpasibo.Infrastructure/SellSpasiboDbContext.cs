using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SellSpasibo.Domain.Entities;
using SellSpasibo.Domain.Repository;

namespace SellSpasibo.Infrastructure
{
    public class SellSpasiboDbContext : DbContext, ISellSpasiboRepo, IUnitOfWork
    {
        public SellSpasiboDbContext() : base() { }
        public SellSpasiboDbContext(DbContextOptions options) : base(options) { }
        public DbSet<UserInfoEntity> UserInfos { get; protected set; }
        public DbSet<TransactionEntity> Transactions { get; protected set; }
        public DbSet<TransactionHistoryEntity> TransactionHistories {get; protected set;}
        public DbSet<BankEntity> Banks { get; protected set; }
        public DbSet<TinkoffAccountEntity> TinkoffAccounts { get; protected set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfoEntity>()
                .HasKey(x => x.Id).HasName("IX_USER_INFO");
            modelBuilder.Entity<UserInfoEntity>()
                .HasIndex(x => x.Phone).IsUnique();

            modelBuilder.Entity<TransactionEntity>()
                .HasKey(x => x.Id).HasName("IX_TRANSACTION");
            modelBuilder.Entity<TransactionEntity>()
                .HasIndex(x => new {x.Time, x.TransactionType})
                .IsUnique();
            
            modelBuilder.Entity<TransactionHistoryEntity>()
                .HasKey(x => x.Id).HasName("IX_TRANSACTION_HISTORY");
            modelBuilder.Entity<TransactionHistoryEntity>()
                .HasIndex(x => new {x.NumberFrom, x.NumberTo});

            modelBuilder.Entity<BankEntity>()
                .HasKey(x => x.Id).HasName("IX_BANK");
            modelBuilder.Entity<BankEntity>()
                .HasIndex(x => x.MemberId)
                .IsUnique();

            modelBuilder.Entity<TinkoffAccountEntity>()
                .HasKey(x => x.Id).HasName("IX_TINKOFF_ACCOUNT");
            modelBuilder.Entity<TinkoffAccountEntity>()
                .HasIndex(x => x.Phone)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("User ID=postgres;Password=root;Server=localhost;Port=5432;Database=sell_spasibo;Integrated Security=true;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public IQueryable<BankEntity> GetBanks()
            => Banks;

        public async Task AddOrUpdateBanks(BankEntity[] banks)
        {
            var existingBanks = Banks.Where(x => !x.IsDeleted);
            
            foreach (var bankItem in banks)
            {
                var existingItem = await existingBanks.FirstOrDefaultAsync(x => x.Name == bankItem.Name);
                
                if (existingItem != null)
                {
                    existingItem.SetMemberId(bankItem.MemberId);
                    Update(existingItem);
                }
                else
                {
                    Banks.Add(bankItem);
                }
            }
        }

        public IQueryable<TransactionEntity> GetTransactions(bool? isPaid = null)
        {
            var existingTransactions = Transactions
                .Where(x => !x.IsDeleted);

            if (isPaid != null)
            {
                existingTransactions = existingTransactions.Where(x => x.IsPaid == isPaid);
            }

            return existingTransactions;
        }

        public async Task AddOrUpdateTransaction(TransactionEntity transaction)
        {
            var existingTransactions = Transactions.Where(x => !x.IsDeleted);

            var existingTransaction = await existingTransactions.FirstOrDefaultAsync(x =>
                x.Time == transaction.Time &&
                x.TransactionType == transaction.TransactionType);
            if (existingTransaction != null)
            {
                existingTransaction.Merge(transaction);
                Update(existingTransaction);
            }
            else
            {
                Transactions.Add(transaction);
            }
        }

        public async Task AddTransactionHistory(TransactionHistoryEntity transactionHistory)
        {
            await TransactionHistories.AddAsync(transactionHistory);
        }

        public async Task SetUserInfoInactive(string number)
        {
            var userInfos = UserInfos.Where(x => !x.IsDeleted);
            var existingUserInfo = await userInfos.FirstOrDefaultAsync(x => x.Phone == number);
            
            if (existingUserInfo != null)
            {
                existingUserInfo.SetInactive();
                Update(existingUserInfo);
            }
        }

        public async Task<UserInfoEntity> GetUserInfoByPhoneNumber(string number)
        {
            var userInfos = UserInfos.Where(x => !x.IsDeleted);

            var existingUserInfo = await userInfos.FirstOrDefaultAsync(x => x.Phone == number);
            return existingUserInfo;
        }

        public async Task AddOrUpdateUserInfo(UserInfoEntity userInfo)
        {
            var userInfos = UserInfos.Where(x => !x.IsDeleted);
            var existingUserInfo = await userInfos.FirstOrDefaultAsync(x => x.Phone == userInfo.Phone);

            if (existingUserInfo != null)
            {
                existingUserInfo.Merge(userInfo);
                Update(existingUserInfo);
            }
            else
            {
                UserInfos.Add(userInfo);
            }
        }

        public IQueryable<TinkoffAccountEntity> GetTinkoffAccounts()
        {
            return TinkoffAccounts.Where(x => !x.IsDeleted);
        }

        public async Task<TinkoffAccountEntity> GetTinkoffAccount(string number)
        {
            return await TinkoffAccounts.FirstOrDefaultAsync(x => x.Phone == number);
        }

        public async Task AddOrUpdateTinkoffAccount(TinkoffAccountEntity account)
        {
            var tinkoffAccounts = TinkoffAccounts.Where(x => !x.IsDeleted);

            var existingAccount = await tinkoffAccounts.FirstOrDefaultAsync(x => x.Phone == account.Phone);

            if (existingAccount != null)
            {
                existingAccount.SetPassword(account.Password);
                existingAccount.SetAccountId(account.AccountId);
                Update(existingAccount);
            }
            else
            {
                TinkoffAccounts.Add(account);
            }
        }

        #region Sealed
        public override int SaveChanges()
        {
            throw new NotSupportedException();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new NotSupportedException();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}
