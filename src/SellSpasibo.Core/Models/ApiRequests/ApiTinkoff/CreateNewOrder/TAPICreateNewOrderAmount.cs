using System.Text.Json.Serialization;
using SellSpasibo.Core.Models.ModelsJson.Tinkoff;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.CreateNewOrder
{
    public class TAPICreateNewOrderAmount
    {
        [JsonPropertyName("currency")]
        public TAPICurrency Currency { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }
    }
}
