using System;

namespace SellSpasibo.BLL.Models
{
    public class Transaction
    {
        public string Number { get; set; }
        public string BankName { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Cost { get; set; }
    }
}
