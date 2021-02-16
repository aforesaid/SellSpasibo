using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.Tinkoff.NewOrder
{
    public class TinkoffSendOrderJson
    {
        [JsonPropertyName("paymentId")]
        public string PaymentId { get; set; }
        public TinkoffPayloadByNewOrder Payload { get; set; }
    }
}
