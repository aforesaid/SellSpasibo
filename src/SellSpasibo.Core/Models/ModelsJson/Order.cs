using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson
{
    public class Order
    {
        [JsonPropertyName("account")]
        public string Account { get; set; }
        [JsonPropertyName("moneyAmount")]
        public double Money { get; set; }
        [JsonPropertyName("provider")]
        public string Provider { get; set; } = "p2p-anybank";
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = "RUB";
        [JsonPropertyName("providerFields")]
        public PaymentDetails Details { get; set; }
    }
}
