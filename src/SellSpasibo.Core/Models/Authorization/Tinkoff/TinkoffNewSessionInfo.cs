namespace SellSpasibo.Core.Models.Authorization.Tinkoff
{
    public class TinkoffNewSessionInfo
    {
        public TinkoffNewSessionInfo(string sessionId, string operationTicket)
        {
            SessionId = sessionId;
            OperationTicket = operationTicket;
        }

        public string SessionId { get; }
        public string OperationTicket { get; }
    }
}