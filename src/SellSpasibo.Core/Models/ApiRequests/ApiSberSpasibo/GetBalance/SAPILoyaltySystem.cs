using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.GetBalance
{
    public class SAPILoyaltySystem
    {
        [JsonPropertyName("balance")]
        public double Balance { get; set; }
    }
}
