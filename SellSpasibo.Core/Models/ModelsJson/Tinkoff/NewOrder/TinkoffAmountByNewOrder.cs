using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.NewOrder
{
    public class TinkoffAmountByNewOrder
    {
        [JsonPropertyName("currency")]
        public TinkoffCurrency Currency { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }
    }
}
