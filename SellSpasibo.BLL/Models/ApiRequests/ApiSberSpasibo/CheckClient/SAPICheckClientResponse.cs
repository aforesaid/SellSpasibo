using System.Text.Json.Serialization;
using SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.Base;

namespace SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.CheckClient
{
    public class SAPICheckClientResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public SAPICheckClientData Data { get; set; }
    }
}
