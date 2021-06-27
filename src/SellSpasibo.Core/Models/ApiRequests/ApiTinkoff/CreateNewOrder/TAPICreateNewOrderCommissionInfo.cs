using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.CreateNewOrder
{
    public class TAPICreateNewOrderCommissionInfo
    {
        [JsonPropertyName("amount")]
        public TAPICreateNewOrderAmount Amount { get; set; }
        [JsonPropertyName("amountWithCommission")]
        public TAPICreateNewOrderAmount AmountWithCommission { get; set; }
        [JsonPropertyName("commission")]
        public TAPICreateNewOrderAmount Commission { get; set; }
    }
}
