namespace SellSpasibo.Core.Models.PayerAccounts
{
    public class OrderResponse
    {
        public OrderResponse(double amount, double commission)
        {
            Amount = amount;
            Commission = commission;
        }

        public double Amount { get;  }
        public double Commission { get; }
    }
}