using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff
{
    public class TAPICurrency
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
