namespace SellSpasibo.Core.Models.PayerAccounts
{
    public class TinkoffPayerAccount
    {
        public TinkoffPayerAccount(string login, string password, string accountId)
        {
            Login = login;
            Password = password;
            AccountId = accountId;
        }

        public string Login { get; }
        public string Password { get; }
        public string AccountId { get; private set; }
    }
}