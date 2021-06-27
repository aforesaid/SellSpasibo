using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.CreateNewOrder
{
    public class TAPICreateNewOrderResponse
    {
        [JsonPropertyName("trackingId")]
        public string PaymentId { get; set; }
        [JsonPropertyName("payload")]
        public TAPICreateNewOrderPayload Payload { get; set; }
    }
}
