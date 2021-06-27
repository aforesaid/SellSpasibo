using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.NewOrder
{
    public class TinkoffPayloadByNewOrder
    {
        [JsonPropertyName("commissionInfo")]
        public TinkoffCommissionInfo CommissionInfo { get; set; }
    }
}
