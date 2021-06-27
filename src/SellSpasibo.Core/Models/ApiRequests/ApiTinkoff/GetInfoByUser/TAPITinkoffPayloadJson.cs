using System.Collections.Generic;
using System.Text.Json.Serialization;
using SellSpasibo.Core.Models.ModelsJson.Tinkoff.UserByBank;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetInfoByUser
{
    public class TAPITinkoffPayloadJson
    {
        [JsonPropertyName("pointerLinkId")]
        public string PointerLinkId { get; set; }
        [JsonPropertyName("displayFields")]
        public List<TAPITinkoffDisplayInfoByUserJson> DisplayInfo { get; set; }
    }
}
