using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.Base
{
    public class ApiError
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("messages")]
        public string[] Messages { get; set; }
    }
}