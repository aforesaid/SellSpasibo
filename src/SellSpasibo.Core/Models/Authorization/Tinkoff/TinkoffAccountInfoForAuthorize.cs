namespace SellSpasibo.Core.Models.Authorization.Tinkoff
{
    public class TinkoffAccountInfoForAuthorize
    {
        public TinkoffAccountInfoForAuthorize(string number, string password, string accountId)
        {
            Number = number;
            Password = password;
            AccountId = accountId;
        }

        public string Number { get; set; }
        public string Password { get; set; }
        public string AccountId { get; set; }
    }
}