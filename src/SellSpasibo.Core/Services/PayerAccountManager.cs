using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Domain.Entities;
using SellSpasibo.Domain.Repository;

namespace SellSpasibo.Core.Services
{
    public class PayerAccountManager
    {
        private readonly ISellSpasiboRepo _sellSpasiboRepo;
        private readonly ILogger<PayerAccountManager> _logger;
        private readonly IStringCrypt _stringCrypt;
        private readonly IUnitOfWork _unitOfWork;
        public PayerAccountManager(ISellSpasiboRepo sellSpasiboRepo, 
            ILogger<PayerAccountManager> logger,
            IStringCrypt stringCrypt, IUnitOfWork unitOfWork)
        {
            _sellSpasiboRepo = sellSpasiboRepo;
            _logger = logger;
            _stringCrypt = stringCrypt;
            _unitOfWork = unitOfWork;
        }

        public async Task AddTinkoffAccount(string phone, string password, string accountId)
        {
            try
            {
                var existingAccount = await _sellSpasiboRepo.GetTinkoffAccount(phone);
                phone = _stringCrypt.Encrypt(phone);
                password = _stringCrypt.Encrypt(phone);
                
                if (existingAccount != null)
                {
                    existingAccount.SetAccountId(accountId);
                    existingAccount.SetPassword(password);
                    
                    await _sellSpasiboRepo.AddOrUpdateTinkoffAccount(existingAccount);
                }
                else
                {
                    var account = new TinkoffAccountEntity(password, accountId, password);
                    await _sellSpasiboRepo.AddOrUpdateTinkoffAccount(account);
                }

                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Данные по платёжному аккаунту с номером {0} успешно добавлены", phone);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось добавить платёжный аккаунт с номером {0}", phone);
                throw;
            }
        }

        public async Task<TinkoffAccountEntity[]> GetAccounts()
        {
            var accounts = await _sellSpasiboRepo.GetTinkoffAccounts()
                .AsNoTracking()
                .ToArrayAsync();
            return accounts;
        }

        public async Task<TinkoffAccountEntity> GetAccountByPhone(string phone)
        {
            return await _sellSpasiboRepo.GetTinkoffAccount(phone);
        }
    }
}