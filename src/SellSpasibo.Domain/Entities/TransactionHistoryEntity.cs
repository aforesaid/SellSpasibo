using System;
using System.ComponentModel.DataAnnotations;

namespace SellSpasibo.Domain.Entities
{
    public class TransactionHistoryEntity : Entity
    {
        private const int NumberLength = 12;
        private TransactionHistoryEntity(){}
        public TransactionHistoryEntity(string numberFrom, string numberTo,
            double commission, double amount, Guid transactionEntityId)
        {
            NumberFrom = numberFrom;
            NumberTo = numberTo;
            Commission = commission;
            Amount = amount;
            TransactionEntityId = transactionEntityId;
        }
        [StringLength(NumberLength)]
        public string NumberFrom {get; private set;}
        [StringLength(NumberLength)]
        public string NumberTo {get; private set;}
        public double Commission {get; private set;}
        public double Amount {get; private set;}
        
        public DateTime TransactionTime {get; private set;} = DateTime.Now;
        
        public Guid TransactionEntityId {get; private set;}
        public TransactionEntity TransactionEntity {get; private set;}
    }
}