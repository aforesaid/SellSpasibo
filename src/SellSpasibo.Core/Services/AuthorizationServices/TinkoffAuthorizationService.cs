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
        private readonly ITinkoffApiClient _tinkoffApiClient;

        public TinkoffAuthorizationService(ILogger<TinkoffAuthorizationService> logger,
            IAccountObserver accountObserver,
            ITinkoffApiClient tinkoffApiClient)
        {
            _logger = logger;
            _accountObserver = accountObserver;
            _tinkoffApiClient = tinkoffApiClient;
        }

        public async Task StartAuthorizeInAccount(string login, string password, int accountId)
        {
            if (AccountsInProgress.ContainsKey(login))
            {
                _logger.LogWarning(
                    "Не удалось создать запрос по авторизации нового аккаунта так как он уже существует с login : {0}",
                    login);
                return;
            }

            var response = await _tinkoffApiClient.SendSms(login, password);
            if (!response)
            {
                _logger.LogError("Не удалось отправить смс, отклоняю добавление");
                return;
            }
            
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
            
            var options = await _tinkoffApiClient.Authorize(account.Login, account.Password, code);

            var accountObserver = new TinkoffObserverAccount(options.WuId, options.SessionId,
                account.AccountId.ToString(), account.Login);
            _accountObserver.AddAccount(accountObserver);
        }
    }
}