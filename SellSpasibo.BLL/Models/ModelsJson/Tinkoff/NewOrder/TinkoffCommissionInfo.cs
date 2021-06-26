using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.Tinkoff.NewOrder
{
    public class TinkoffCommissionInfo
    {
        [JsonPropertyName("amount")]
        public TinkoffAmountByNewOrder Amount { get; set; }
        [JsonPropertyName("amountWithCommission")]
        public TinkoffAmountByNewOrder AmountWithCommission { get; set; }
        [JsonPropertyName("commission")]
        public TinkoffAmountByNewOrder Commission { get; set; }
    }
}
