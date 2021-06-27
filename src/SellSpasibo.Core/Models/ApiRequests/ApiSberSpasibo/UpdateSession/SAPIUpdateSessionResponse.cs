using System.Text.Json.Serialization;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.Base;

namespace SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.UpdateSession
{
    public class SAPIUpdateSessionResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public SAPIUpdateSessionData Info { get; set; }
    }

}
