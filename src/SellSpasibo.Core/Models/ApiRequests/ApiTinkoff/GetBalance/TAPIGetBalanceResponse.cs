using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBalance
{
    public class TAPIGetBalanceResponse
    {
        [JsonPropertyName("trackingId")]
        public string TrackingId { get; set; }
        [JsonPropertyName("payload")]
        public TAPIGetBalancePayloadInfo Payload { get; set; }
    }
}
