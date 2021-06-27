using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBalance
{
    public class TAPIGetBalanceInfoByCard
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("accountBalance")]
        public TAPIGetBalanceAccountBalance AccountBalance { get; set; }
    }
}
