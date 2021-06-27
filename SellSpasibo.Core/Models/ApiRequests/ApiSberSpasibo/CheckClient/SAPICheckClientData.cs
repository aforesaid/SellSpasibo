using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.CheckClient
{
    public class SAPICheckClientData
    {
        [JsonPropertyName("maxSum")]
        public double MaxSum { get; set; }
        [JsonPropertyName("minSum")]
        public double MinSum { get; set; }
    }
}
