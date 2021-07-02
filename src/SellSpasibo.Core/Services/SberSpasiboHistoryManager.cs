using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Domain.Entities;
using SellSpasibo.Domain.Repository;

namespace SellSpasibo.Core.Services
{
    public class SberSpasiboHistoryManager : ISberSpasiboHistoryManager
    {
        private readonly ISberSpasiboApiClient _sberSpasiboApiClient;
        
        private readonly ISellSpasiboRepo _sellSpasiboRepo;
        private readonly IUnitOfWork _unitOfWork;
        
        private readonly ILogger<SberSpasiboHistoryManager> _logger;

        public SberSpasiboHistoryManager(ISberSpasiboApiClient sberSpasiboApiClient, 
            ISellSpasiboRepo sellSpasiboRepo, 
            IUnitOfWork unitOfWork,
            ILogger<SberSpasiboHistoryManager> logger)
        {
            _sberSpasiboApiClient = sberSpasiboApiClient;
            _sellSpasiboRepo = sellSpasiboRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task UpdateTransactionsHistory()
        {
            var transactions = await _sberSpasiboApiClient.GetTransactionHistory();
            const string transactionType = "BONUS";
            
            foreach (var transaction in transactions)
            {
                await _sellSpasiboRepo.AddOrUpdateTransaction(new TransactionEntity(transaction.BonusBalanceChange,
                    transactionType,
                    transaction.PartnerName, DateTime.Parse(transaction.Date)));
                await _unitOfWork.SaveChangesAsync();
            }
            _logger.LogInformation("Запрос обновления истории переводов бонусов сберспасибо выполнен. Получено было {0} транзакций",
                transactions.Length);
        }
    }
}