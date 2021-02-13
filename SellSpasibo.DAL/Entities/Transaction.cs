using System;
using System.ComponentModel.DataAnnotations;

namespace SellSpasibo.DAL.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public string TransactionType { get; set; }
        public DateTime Time { get; set; }
        public string Hash { get; set; }
    }
}
