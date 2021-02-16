using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.Tinkoff.Balance
{
    class TinkoffPayload
    {
        [JsonPropertyName("payload")]
        public List<TinkoffInfoByCard> Cards { get; set; }
        [JsonPropertyName("resultCode")]
        public string ResultCode { get; set; }
    }
}
