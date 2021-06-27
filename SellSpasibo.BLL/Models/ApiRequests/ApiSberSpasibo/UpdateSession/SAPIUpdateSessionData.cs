using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.UpdateSession
{
    public class SAPIUpdateSessionData
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}