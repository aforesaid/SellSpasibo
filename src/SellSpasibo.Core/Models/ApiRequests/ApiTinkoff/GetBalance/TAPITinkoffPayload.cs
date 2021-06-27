using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.Balance
{
    public class TAPITinkoffPayload
    {
        [JsonPropertyName("payload")]
        public List<TAPITinkoffInfoByCard> Cards { get; set; }
        [JsonPropertyName("resultCode")]
        public string ResultCode { get; set; }
    }

    public class TinkoffPayloadInfo
    {
        [JsonPropertyName("0")]
        public TAPITinkoffPayload Payload { get; set; }
    }
}
