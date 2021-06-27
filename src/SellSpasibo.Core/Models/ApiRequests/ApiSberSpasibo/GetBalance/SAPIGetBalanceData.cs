using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.GetBalance
{
    public class SAPIGetBalanceData
    {
        [JsonPropertyName("loyaltySystem")]
        public SAPILoyaltySystem LoyaltySystem { get; set; }
    }
}
