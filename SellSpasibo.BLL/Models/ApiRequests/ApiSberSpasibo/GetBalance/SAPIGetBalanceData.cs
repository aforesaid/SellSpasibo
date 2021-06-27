using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.GetBalance
{
    public class SAPIGetBalanceData
    {
        [JsonPropertyName("loyaltySystem")]
        public SAPILoyaltySystem LoyaltySystem { get; set; }
    }
}
