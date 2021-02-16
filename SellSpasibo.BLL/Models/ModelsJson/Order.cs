using System.Text.Json;
using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson
{
    public class Order
    {
        [JsonPropertyName("account")]
        public string Account { get; set; }
        [JsonPropertyName("moneyAmount")]
        public decimal Money { get; set; }
        [JsonPropertyName("provider")]
        public string Provider { get; set; } = "p2p-anybank";
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = "RUB";
        [JsonPropertyName("providerFields")]
        public PaymentDetails Details { get; set; }

        public override string ToString()
            => JsonSerializer.Serialize(this);
    }
}
