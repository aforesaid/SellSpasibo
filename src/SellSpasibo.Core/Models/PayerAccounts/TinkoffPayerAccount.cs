namespace SellSpasibo.Core.Models.PayerAccounts
{
    public class TinkoffPayerAccount
    {
        public TinkoffPayerAccount(string login, string password, int accountId)
        {
            Login = login;
            Password = password;
            AccountId = accountId;
        }

        public string Login { get; }
        public string Password { get; }
        public int AccountId { get; private set; }
    }
}