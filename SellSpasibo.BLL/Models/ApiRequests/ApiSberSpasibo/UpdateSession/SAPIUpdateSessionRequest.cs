namespace SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.UpdateSession
{
    public class SAPIUpdateSessionRequest
    {
        public SAPIUpdateSessionRequest(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
        public string RefreshToken { get; }
    }
}