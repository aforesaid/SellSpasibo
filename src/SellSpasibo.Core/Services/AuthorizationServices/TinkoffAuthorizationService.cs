using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Core.Interfaces.AuthorizationService;
using SellSpasibo.Core.Models.ObserverAccounts;
using SellSpasibo.Core.Models.PayerAccounts;
using SellSpasibo.Core.Options;

namespace SellSpasibo.Core.Services.AuthorizationServices
{
    public class TinkoffAuthorizationService : ITinkoffAuthorizationService
    {
        private ConcurrentDictionary<string, TinkoffPayerAccount> AccountsInProgress = new();
        private readonly ILogger<TinkoffAuthorizationService> _logger;
        private readonly IAccountObserver _accountObserver;

        public async Task StartAuthorizeInAccount(string login, string password, int accountId)
        {
            if (AccountsInProgress.ContainsKey(login))
            {
                _logger.LogWarning(
                    "Не удалось создать запрос по авторизации нового аккаунта так как он уже существует с login : {0}",
                    login);
                return;
            }

            //TODO: отправка смс все дела
            var account = new TinkoffPayerAccount(login, password, accountId);
            var result = AccountsInProgress.TryAdd(account.Login, account);
            if (!result)
            {
                _logger.LogError("Не удалось начать авторизацию по аккаунту с логином {0}", account.Login);
            }
        }

        public async Task ContinueAuthorize(string login, string code)
        {
            var result = AccountsInProgress.TryGetValue(login, out var account);
            if (!result)
            {
                _logger.LogError("Аккаунта с логином {0} не было в списке предподготовленных на авторизацию");
                return;
            }

            //TODO: отправка кода и получение данных авторизации по аккаунту
            TinkoffOptions options = null;

            var accountObserver = new TinkoffObserverAccount(options.WuId, options.SessionId,
                account.AccountId.ToString(), account.Login);
            _accountObserver.AddAccount(accountObserver);
        }
    }
}