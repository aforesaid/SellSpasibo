using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Core.Models;
using SellSpasibo.Core.Models.ObserverAccounts;

namespace SellSpasibo.Core.Services
{
    public class AccountObserver : IAccountObserver
    {
        private readonly ConcurrentDictionary<string, TinkoffObserverAccount> _activeTinkoffObserverAccounts = new();

        private readonly ITinkoffApiClient _tinkoffApiClient;
            
        private readonly ILogger<AccountObserver> _logger;

        public AccountObserver(ILogger<AccountObserver> logger,
            ITinkoffApiClient tinkoffApiClient)
        {
            _logger = logger;
            _tinkoffApiClient = tinkoffApiClient;
        }
        /// <summary>
        /// Обновление токенов
        /// </summary>
        public async Task UpdateAccountsInfo()
        {
            var accounts = _activeTinkoffObserverAccounts.Select(x => x.Value);
            foreach (var account in accounts.Where(x => x.IsWorking))
            {
                _tinkoffApiClient.SetTokens(account.SessionId, account.Wuid, account.AccountId);
                var result = await _tinkoffApiClient.UpdateSession();
                if (!result)
                {
                    account.SetInactive();
                    _logger.LogError("Один из аккаунтов больше не работает, не удалось обновить сессию {0}", account);
                }
                var balance = await _tinkoffApiClient.GetBalance();
                var currentBalance = balance.Payload?.Payload?.Cards?.FirstOrDefault(x => x.Id == account.AccountId);
                if (currentBalance == null)
                {
                    _logger.LogWarning("Не удалось обновить баланс по аккаунту {0}", account);
                }
                else
                {
                    account.UpdateMoney(currentBalance.AccountBalance.Value);
                }
            }
        }
        /// <summary>
        /// Нужно для транзакции подготовить несколько аккаунтов
        /// </summary>
        /// <returns></returns>
        public OrderAccounts SelectAccountsForTransaction(double transactionMoney)
        {
            var accounts = _activeTinkoffObserverAccounts
                .Select(x => x.Value)
                .OrderBy(x => x.Money);
            
            if (accounts.Sum(x => x.Money) < transactionMoney)
            {
                _logger.LogError("Не удалось выбрать аккаунты для платежа в {0} рублей",
                    transactionMoney);
                return null;
            }

            var result = new List<OrderAccountInfo>();
            
            foreach (var account in accounts)
            {
                if (account.Money <= 0)
                    continue;
                
                var lockedMoney = account.Money > transactionMoney ? transactionMoney : account.Money;
                account.LockMoney(lockedMoney);

                transactionMoney -= lockedMoney;
                result.Add(new OrderAccountInfo(account, lockedMoney));

                if (transactionMoney == 0)
                    break;
            }

            if (transactionMoney == 0)
            {
                _logger.LogInformation("Для запроса в {0} рублей было выбрано {1} аккаунтов", 
                    transactionMoney, result.Count);
            }
            else
            {
                _logger.LogInformation("Для запроса осталось ещё {1} необслужанных рублей, добавлю в заявки",
                    transactionMoney);
            }

            return new OrderAccounts(result.ToArray());
        }
        /// <summary>
        /// Действие после проведения платежа, разблокировка заблокированных денег
        /// </summary>
        /// <param name="orderAccounts"></param>

        public void UnlockOrRemoveMoneyFromAccounts(OrderAccounts orderAccounts)
        {
            foreach (var account in orderAccounts.Accounts)
            {
                var activeAccount = _activeTinkoffObserverAccounts
                    .FirstOrDefault(x => x.Key == account.Account.Number).Value;
                
                if (activeAccount == null)
                {
                    _logger.LogError("Аккаунт c номером {0} не был найден для разблокировки денег");
                    continue;
                }

                var result = account.Status switch
                {
                    TransactionStatusEnum.MoneySent => activeAccount.RemoveMoney(account.Money),
                    TransactionStatusEnum.MoneyNotSent => activeAccount.UnlockMoney(account.Money),
                    _ => false
                };
                if (!result)
                {
                    _logger.LogError("Не удалось совершить действие по статусу {0} с аккаунтом {@1}",
                        account.Status, account.Account);
                }
            }
        }

        /// <summary>
        /// Процесс добавления аккаунта в активные
        /// </summary>
        /// <param name="number"></param>
        /// <param name="account"></param>
        public bool AddAccount(TinkoffObserverAccount account)
        {
            if (account == null)
            {
                _logger.LogError("Попытка добавления аккаунта = null, отклоняю");
                return false;
            }
            
            if (_activeTinkoffObserverAccounts.ContainsKey(account.Number))
            {
                _logger.LogError("Платёжный аккаунт с номером {0} уже добавлён, пропускаю запрос",
                    account.Number);
                return false;
            }

            var result = _activeTinkoffObserverAccounts.TryAdd(account.Number, account);
            
            if (result)
            {
                _logger.LogInformation("Успешно добавлен в список активных платёжных аккаунтов аккаунт с номером {0}",
                    account.Number);
            }
            else
            {
                _logger.LogInformation("Не удалось добавить аккаунт в список активных платёжных аккаунтов, Number : {0}",
                    account.Number);
            }

            return result;
        } 
    }
}