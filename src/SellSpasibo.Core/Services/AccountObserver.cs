using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SellSpasibo.Core.Models.ObserverAccounts;

namespace SellSpasibo.Core.Services
{
    public class AccountObserver
    {
        public ConcurrentQueue<TinkoffObserverAccount> ActiveTinkoffObserverAccounts { get; } = new();

        private readonly ILogger<AccountObserver> _logger;

        public AccountObserver(ILogger<AccountObserver> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Обновление токенов
        /// </summary>
        public async Task UpdateAccountsTokens()
        {
            
        }
        /// <summary>
        /// Обновление суммы на аккаунте и транзакций
        /// </summary>
        public async Task UpdateAccountsTransactions()
        {
            
        }
        /// <summary>
        /// Нужно для транзакции подготовить несколько аккаунтов
        /// </summary>
        /// <returns></returns>
        public async Task<TinkoffObserverAccount[]> SelectAccountsForTransaction()
        {
            return null;
        }
    }
}