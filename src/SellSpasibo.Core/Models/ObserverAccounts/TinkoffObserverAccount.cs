namespace SellSpasibo.Core.Models.ObserverAccounts
{
    public class TinkoffObserverAccount
    {
        private object LockObject { get; }
        
        public string Number { get; }
        public string Wuid { get; }
        public string SessionId { get; }
        public string AccountId { get; }
        public double Money { get; private set; }

        public double LockedMoney { get; private set; }

        public TinkoffObserverAccount(string wuid, string sessionId, string accountId,
            string number)
        {
            Wuid = wuid;
            SessionId = sessionId;
            AccountId = accountId;
            Number = number;
        }

        /// <summary>
        /// Количество пользователей, о которых была получена информация за сутки, лимит 20
        /// </summary>
        public int CountNewUsers { get; private set; }

        public bool IsWorking { get; private set; }

        public void UpdateCountNewUsers()
        {
            CountNewUsers++;
        }

        public void UpdateMoney(double money)
        {
            Money = money - LockedMoney;
        }


        public void SetInactive()
        {
            IsWorking = false;
        }

        public bool LockMoney(double lockedMoney)
        {
            lock (LockObject)
            {
                if (Money < lockedMoney)
                    return false;
                
                Money -= lockedMoney;
                LockedMoney += lockedMoney;

                return true;
            }
        }

        public bool UnlockMoney(double lockedMoney)
        {
            if (LockedMoney < lockedMoney)
                return false;
            LockedMoney -= lockedMoney;
            return true;
        }

        public bool RemoveMoney(double lockedMoney)
        {
            if (LockedMoney < lockedMoney)
                return false;
            
            LockedMoney -= lockedMoney;
            
            return true;
        }
    }
}