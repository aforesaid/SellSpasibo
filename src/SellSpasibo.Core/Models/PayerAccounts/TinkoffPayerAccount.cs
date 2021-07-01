namespace SellSpasibo.Core.Models.PayerAccounts
{
    public class TinkoffPayerAccount
    {
        public TinkoffPayerAccount(string login, string password, int accountId, string sessionId, string operationTicket)
        {
            Login = login;
            Password = password;
            AccountId = accountId;
            SessionId = sessionId;
            OperationTicket = operationTicket;
        }
        
        public string Login { get; }
        public string Password { get; }
        public int AccountId { get; private set; }
        public string SessionId { get; }
        public string OperationTicket { get; }
    }
}