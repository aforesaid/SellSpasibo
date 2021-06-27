using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.NewOrder
{
    public class TAPITinkoffPayloadByNewOrder
    {
        [JsonPropertyName("commissionInfo")]
        public TAPITinkoffCommissionInfo CommissionInfo { get; set; }
    }
}
