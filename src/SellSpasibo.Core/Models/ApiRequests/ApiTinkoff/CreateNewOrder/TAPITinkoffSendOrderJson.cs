using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.NewOrder
{
    public class TAPITinkoffSendOrderJson
    {
        [JsonPropertyName("trackingId")]
        public string PaymentId { get; set; }
        [JsonPropertyName("payload")]
        public TAPITinkoffPayloadByNewOrder Payload { get; set; }
    }
}
