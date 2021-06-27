using System.Text.Json.Serialization;
using SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.Base;

namespace SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.UpdateSession
{
    public class SAPIUpdateSessionResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public SAPIUpdateSessionData Info { get; set; }
    }

}
