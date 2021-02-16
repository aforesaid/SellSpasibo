using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.Tinkoff.UserByBank
{
    public class TinkoffPayloadJson
    {
        [JsonPropertyName("pointerLinkId")]
        public string PointerLinkId { get; set; }
        [JsonPropertyName("displayField")]
        public List<TinkoffDisplayInfoByUserJson> DisplayInfo { get; set; } = new List<TinkoffDisplayInfoByUserJson>();
    }
}
