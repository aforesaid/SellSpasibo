using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.GetBalance
{
    public class SAPILoyaltySystem
    {
        [JsonPropertyName("balance")]
        public double Balance { get; set; }
    }
}
