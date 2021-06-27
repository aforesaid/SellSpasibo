using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.Balance
{
    public class TAPITinkoffAccountBalance
    {
        [JsonPropertyName("currency")]
        public TAPITinkoffCurrency Currency { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }
    }
}
