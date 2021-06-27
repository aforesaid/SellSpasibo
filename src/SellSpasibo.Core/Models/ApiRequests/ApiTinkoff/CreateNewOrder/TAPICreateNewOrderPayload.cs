using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.CreateNewOrder
{
    public class TAPICreateNewOrderPayload
    {
        [JsonPropertyName("commissionInfo")]
        public TAPICreateNewOrderCommissionInfo CommissionInfo { get; set; }
    }
}
