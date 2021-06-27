using System;

namespace SellSpasibo.Core.Models
{
    public class Transaction
    {
        public string Number { get; set; }
        public string BankName { get; set; }
        public DateTime DateTime { get; set; }
        public double Cost { get; set; }
    }
}
