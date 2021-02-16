using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SellSpasibo.BLL.Models.ModelsJson;
using SellSpasibo.BLL.Models.ModelsJson.Tinkoff.NewOrder;
using SellSpasibo.DAL;
using SellSpasibo.DAL.Entities;
using Transaction = SellSpasibo.BLL.Models.Transaction;

namespace SellSpasibo.BLL.Services
{
    public class TransactionService
    {
        private readonly SellSpasiboDbContext _context;

        public TransactionService(SellSpasiboDbContext context)
        {
            _context = context;
        }

        public async Task<TinkoffSendOrderJson> CreateNewSberSpasiboOrder(Transaction transaction)
        {
            if (await IsValid(transaction))
            {
                var bank = await GetInfoByBank(transaction.BankName);
                if (bank == null)
                    return null;
                var paymentDetails = new PaymentDetails()
                {
                    Pointer = $"+{transaction.Number}",
                    MaskedFIO = bank.MemberId
                };
                var order = new Order()
                {
                    Money = Math.Truncate(transaction.Cost * 0.7m),
                    Details = paymentDetails
                };
                var tinkoffService = new Tinkoff();
                var response = await tinkoffService.CreateNewOrder(order);
            }
            return null;
        }

        /// <summary>
        /// Проверка на наличие неоплаченного ордера в БД
        /// </summary>
        /// <param name="transaction">Детали операции</param>
        /// <returns>True, если в БД есть такая операция</returns>
        private async Task<bool> IsValid(Transaction transaction)
            => await _context.Transactions.AnyAsync(item => item.IsPaid && item.Cost == transaction.Cost
                                                             && item.Time == transaction.DateTime);
        /// <summary>
        /// Поиск банка в БД
        /// </summary>
        /// <param name="nameBank">Название искомого банка</param>
        /// <returns>Детальная информация по банку</returns>
        private async Task<Bank> GetInfoByBank(string nameBank)
            => await _context.Banks.FirstOrDefaultAsync(item => item.Name == nameBank);

    }
}