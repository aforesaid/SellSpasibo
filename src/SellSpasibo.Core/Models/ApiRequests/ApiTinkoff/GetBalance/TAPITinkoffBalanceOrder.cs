using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.Balance
{
    public class TAPITinkoffBalanceOrder
    {
        [JsonPropertyName("trackingId")]
        public string TrackingId { get; set; }
        [JsonPropertyName("payload")]
        public TinkoffPayloadInfo Payload { get; set; }
    }
}
