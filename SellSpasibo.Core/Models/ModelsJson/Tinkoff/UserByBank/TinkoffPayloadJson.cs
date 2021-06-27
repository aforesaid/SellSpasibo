using System.Collections.Generic;
using System.Text.Json.Serialization;
using SellSpasibo.Core.Models.ModelsJson.Tinkoff.Balance;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.UserByBank
{
    public class TinkoffPayloadJson
    {
        [JsonPropertyName("pointerLinkId")]
        public string PointerLinkId { get; set; }
        [JsonPropertyName("displayFields")]
        public List<TinkoffDisplayInfoByUserJson> DisplayInfo { get; set; } = new List<TinkoffDisplayInfoByUserJson>();
    }
}
