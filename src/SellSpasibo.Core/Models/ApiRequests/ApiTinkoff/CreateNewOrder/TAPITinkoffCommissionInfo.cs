using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.NewOrder
{
    public class TAPITinkoffCommissionInfo
    {
        [JsonPropertyName("amount")]
        public TAPITinkoffAmountByNewOrder Amount { get; set; }
        [JsonPropertyName("amountWithCommission")]
        public TAPITinkoffAmountByNewOrder AmountWithCommission { get; set; }
        [JsonPropertyName("commission")]
        public TAPITinkoffAmountByNewOrder Commission { get; set; }
    }
}
