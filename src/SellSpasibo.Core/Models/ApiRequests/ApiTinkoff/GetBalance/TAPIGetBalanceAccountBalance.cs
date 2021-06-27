using System.Text.Json.Serialization;
namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBalance
{
    public class TAPIGetBalanceAccountBalance
    {
        [JsonPropertyName("currency")]
        public TAPICurrency Currency { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }
    }
}
