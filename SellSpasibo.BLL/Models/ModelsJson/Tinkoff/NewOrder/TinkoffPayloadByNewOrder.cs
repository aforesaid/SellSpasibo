using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.Tinkoff.NewOrder
{
    public class TinkoffPayloadByNewOrder
    {
        [JsonPropertyName("commissionInfo")]
        public TinkoffCommissionInfo CommissionInfo { get; set; }
    }
}
