using System.ComponentModel.DataAnnotations;

namespace SellSpasibo.Domain.Entities
{
    public class PayInfoEntity : Entity
    {
        private const int NumberLength = 12;
        
        private PayInfoEntity() { }

        public PayInfoEntity(string number, double amount)
        {
            Number = number;
            Amount = amount;
        }
        [StringLength(NumberLength)]
        public string Number { get; private set; }
        public double Amount { get; private set; }
        public bool Status { get; private set; }

        public void SetSuccessStatus(bool status)
        {
            Status = status;
            SetUpdated();
        }
    }
}