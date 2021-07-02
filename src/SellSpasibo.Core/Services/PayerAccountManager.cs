using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Core.Models;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.CreateNewOrder;
using SellSpasibo.Core.Models.ObserverAccounts;
using SellSpasibo.Core.Models.PayerAccounts;
using SellSpasibo.Domain.Entities;
using SellSpasibo.Domain.Repository;

namespace SellSpasibo.Core.Services
{
    public class PayerAccountManager : IPayerAccountManager
    {
        private ConcurrentQueue<PayInfo> _payInfos = new();
        
        private readonly ILogger<PayerAccountManager> _logger;
        private readonly IAccountObserver _accountObserver;
        private readonly ISellSpasiboRepo _sellSpasiboRepo;
        private readonly ITinkoffApiClient _tinkoffApiClient;
        private readonly IUnitOfWork _unitOfWork;
        public PayerAccountManager(ILogger<PayerAccountManager> logger, IAccountObserver accountObserver,
            ISellSpasiboRepo sellSpasiboRepo, IUnitOfWork unitOfWork,
            ITinkoffApiClient tinkoffApiClient)
        {
            _logger = logger;
            _accountObserver = accountObserver;
            _sellSpasiboRepo = sellSpasiboRepo;
            _unitOfWork = unitOfWork;
            _tinkoffApiClient = tinkoffApiClient;
        }

        public async Task<bool> SendMoney(string number, double amount)
        {
            //На данном этапе подразумевается, что данные уже есть в БД
            var userInfo = await _sellSpasiboRepo.GetUserInfoByPhoneNumber(number);
            if (userInfo == null)
            {
                _logger.LogError("Данные по аккаунту не были найдены в БД, отклоняю операцию");
                return false;
            }
            
            var accounts = _accountObserver.SelectAccountsForTransaction(amount);
            if (accounts.Money < amount)
            {
                _payInfos.Enqueue(new PayInfo(number, amount - accounts.Money));
                return true;
            }

            foreach (var accountTransaction in accounts.Accounts)
            {
                var response = await SendMoney(accountTransaction.Account, userInfo, accountTransaction.Money);
                if (response != null)
                {
                    accountTransaction.SetTransactionStatus(TransactionStatusEnum.MoneySent);
                    _logger.LogInformation("Был совершён перевод на номер телефона {0} сумма {1}", 
                        accountTransaction.Account.Number, accountTransaction.Money);
                    //TODO: сохранение перевода в истории
                }
                else
                {
                    accountTransaction.SetTransactionStatus(TransactionStatusEnum.MoneyNotSent);
                    _logger.LogInformation("Был совершён перевод на номер телефона {0} сумма {1}", 
                        accountTransaction.Account.Number, accountTransaction.Money);
                }
            }

            var notSentMoney = accounts.Accounts
                .Where(x => x.Status == TransactionStatusEnum.MoneyNotSent)
                .Sum(x => x.Money);
            
            if (notSentMoney != 0)
            {
                _payInfos.Enqueue(new PayInfo(number, notSentMoney));
                _logger.LogWarning("Не удалось отправить {0} рублей, повторяю запрос", notSentMoney);
            }

            _accountObserver.UnlockOrRemoveMoneyFromAccounts(accounts);
            return true;
        }

        private async Task<OrderResponse> SendMoney(TinkoffObserverAccount accountInfo, UserInfoEntity userInfo, double amount)
        {
            amount = Math.Round(amount, 2);
            
            var order = new TAPICreateNewOrderRequest()
            {
                Money = amount,
                Details = new TAPICreateNewOrdersPaymentDetails()
                {
                    Pointer = $"+{userInfo.Phone}",
                    MaskedFIO = userInfo.Name,
                    PointerLinkId = userInfo.PhoneLinkId
                },
                Account = accountInfo.AccountId
            };
            var response = await _tinkoffApiClient.CreateNewOrder(accountInfo.SessionId, order);
           
            const string successResultCode = "OK";
            if (response.ResultCode != successResultCode)
                return null;
            
            return new OrderResponse(response.Payload.CommissionInfo.Amount.Value,
                response.Payload.CommissionInfo.Commission.Value);
        }
    }
}