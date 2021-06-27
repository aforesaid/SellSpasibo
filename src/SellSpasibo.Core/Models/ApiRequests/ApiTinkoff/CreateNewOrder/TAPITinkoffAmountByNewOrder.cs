using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.NewOrder
{
    public class TAPITinkoffAmountByNewOrder
    {
        [JsonPropertyName("currency")]
        public TAPITinkoffCurrency Currency { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }
    }
}
