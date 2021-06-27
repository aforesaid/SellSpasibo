using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.Balance
{
    public class TinkoffPayload
    {
        [JsonPropertyName("payload")]
        public List<TinkoffInfoByCard> Cards { get; set; }
        [JsonPropertyName("resultCode")]
        public string ResultCode { get; set; }
    }

    public class TinkoffPayloadInfo
    {
        [JsonPropertyName("0")]
        public TinkoffPayload Payload { get; set; }
    }
}
