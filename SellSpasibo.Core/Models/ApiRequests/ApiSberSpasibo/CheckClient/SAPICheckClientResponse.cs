using System.Text.Json.Serialization;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.Base;

namespace SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.CheckClient
{
    public class SAPICheckClientResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public SAPICheckClientData Data { get; set; }
    }
}
