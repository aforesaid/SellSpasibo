using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetInfoByUser
{
    public class TAPIGetInfoByUserResponse
    {
        [JsonPropertyName("resultCode")]
        public string ResultCode { get; set; }
        [JsonPropertyName("payload")]
        public TAPIGetInfoByUserPayload[] Payload { get; set; }
    }
}
