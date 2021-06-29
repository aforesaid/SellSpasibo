namespace SellSpasibo.Core.Models.ObserverAccounts
{
    public class TinkoffObserverAccount
    {
        public string Wuid { get; }
        public string SessionId { get; }
        public string AccountId { get; }
        public double Money { get; private set; }

        public TinkoffObserverAccount(string wuid, string sessionId, string accountId)
        {
            Wuid = wuid;
            SessionId = sessionId;
            AccountId = accountId;
        }
        
        /// <summary>
        /// Количество пользователей, о которых была получена информация за сутки, лимит 20
        /// </summary>
        public int CountNewUsers { get; private set; }

        public void UpdateCountNewUsers()
        {
            CountNewUsers++;
        }

        public void UpdateMoney(double money)
        {
            Money = money;
        }
    }
}