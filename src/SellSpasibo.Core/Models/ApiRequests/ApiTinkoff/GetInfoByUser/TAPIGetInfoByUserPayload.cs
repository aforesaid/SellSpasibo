using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetInfoByUser
{
    public class TAPIGetInfoByUserPayload
    {
        [JsonPropertyName("pointerLinkId")]
        public string PointerLinkId { get; set; }
        [JsonPropertyName("displayFields")]
        public List<TAPIGetInfoByUserDisplayInfo> DisplayInfo { get; set; }
    }
}
