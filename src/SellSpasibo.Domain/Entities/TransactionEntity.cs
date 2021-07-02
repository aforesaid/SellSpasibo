using System;
using System.ComponentModel.DataAnnotations;

namespace SellSpasibo.Domain.Entities
{
    public class TransactionEntity : Entity
    {
        private const int TransactionTypeLength = 20;
        private const int ContentLength = 100;
        private const int HashLength = 100;

        private TransactionEntity() { }
        public TransactionEntity(double cost, string transactionType, 
            string content, DateTime time)
        {
            Cost = cost;
            TransactionType = transactionType;
            Content = content;
            Time = time;
            Hash = (cost +transactionType + content + time).GetHashCode().ToString();
        }

        public double Cost { get; protected set; }
        
        [StringLength(TransactionTypeLength)]
        public string TransactionType { get; protected set; }
        [StringLength(ContentLength)]
        public  string Content { get; protected set; }
        public DateTime Time { get; protected set; }
        [StringLength(HashLength)]
        public string Hash { get; protected set; }
        public bool IsPaid { get; protected set; }

        public void SetIsPaid(bool isPaid)
        {
            IsPaid = isPaid;
            SetUpdated();
        }

        public void Merge(TransactionEntity transaction)
        {
            Content = transaction.Content;
            Hash = transaction.Hash;
        }
    }
}
