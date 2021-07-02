using System;
using System.ComponentModel.DataAnnotations;

namespace SellSpasibo.Domain.Entities
{
    public class PayInfoEntity : Entity
    {
        private const int NumberLength = 12;
        
        private PayInfoEntity(Guid transactionEntityId)
        {
            TransactionEntityId = transactionEntityId;
        }

        public PayInfoEntity(string number, double amount, Guid transactionEntityId)
        {
            Number = number;
            Amount = amount;
            TransactionEntityId = transactionEntityId;
        }
        [StringLength(NumberLength)]
        public string Number { get; private set; }
        public double Amount { get; private set; }
        public double SentAmout { get; private set; }
        public bool Status { get; private set; }
        
        public Guid TransactionEntityId { get; private set; }
        public TransactionEntity TransactionEntity { get; private set; }

        public void SetStatus(bool status, double sentMoney)
        {
            Status = status;
            SentAmout = sentMoney;
            SetUpdated();
        }
    }
}