namespace SellSpasibo.Core.Models.PayerAccounts
{
    public class PayInfo
    {
        public PayInfo(string number, double amount)
        {
            Number = number;
            Amount = amount;
        }

        /// <summary>
        /// Номер без плюса
        /// </summary>
        public string Number { get; }
        /// <summary>
        /// Сумма, которую должны перевести
        /// </summary>
        public double Amount { get; }
    }
}