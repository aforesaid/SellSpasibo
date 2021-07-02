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

        public async Task TrySendAllNotPayingTransaction()
        {
            var notPayingTransactions = _sellSpasiboRepo.GetPayInfosNotPayed()
                .ToArray();
            foreach (var transaction  in notPayingTransactions)
            {
                var result = await SendMoney(transaction.Number, transaction.Amount, transaction.TransactionEntityId);
                
                if (!result.HasValue)
                {
                    _logger.LogError("Не удалось отправить транзакцию в принципе! {@0}", transaction);
                    continue;
                }
                
                transaction.SetStatus(true, result.Value);

                await _sellSpasiboRepo.AddOrUpdatePayInfo(transaction);
            }
        }

        public async Task AddNotPayingTransaction(string number, double amount, Guid transactionId)
        {
            await _sellSpasiboRepo.AddOrUpdatePayInfo(new PayInfoEntity(number, amount, transactionId));
            await _unitOfWork.SaveChangesAsync();
        }


        private async Task<double?> SendMoney(string number, double amount, Guid transactionId)
        {
            //На данном этапе подразумевается, что данные уже есть в БД
            var userInfo = await _sellSpasiboRepo.GetUserInfoByPhoneNumber(number);
            if (userInfo == null)
            {
                _logger.LogError("Данные по аккаунту {0} не были найдены в БД, отклоняю операцию на сумму {1}",
                    number, amount);
                return null;
            }
            
            var accounts = _accountObserver.SelectAccountsForTransaction(amount);

            foreach (var accountTransaction in accounts.Accounts)
            {
                var response = await SendMoney(accountTransaction.Account, userInfo, accountTransaction.Money);
                if (response != null)
                {
                    accountTransaction.SetTransactionStatus(TransactionStatusEnum.MoneySent);
                    _logger.LogInformation("Был совершён перевод на номер телефона {0} сумма {1}", 
                        accountTransaction.Account.Number, accountTransaction.Money);
                    await _sellSpasiboRepo.AddTransactionHistory(new TransactionHistoryEntity(accountTransaction.Account.Number, number,
                        response.Amount, response.Commission, transactionId));
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
                .Sum(x => x.Money) + amount - accounts.Money;
            
            if (notSentMoney != 0)
            {
                await _sellSpasiboRepo.AddOrUpdatePayInfo(new PayInfoEntity(number, notSentMoney, transactionId));
                _logger.LogWarning("Не удалось отправить {0} рублей, повторяю запрос", notSentMoney);
            }

            _accountObserver.UnlockOrRemoveMoneyFromAccounts(accounts);
            await _unitOfWork.SaveChangesAsync();
            
            return notSentMoney;
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