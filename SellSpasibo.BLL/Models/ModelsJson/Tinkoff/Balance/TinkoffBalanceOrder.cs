using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.Tinkoff.Balance
{
    class TinkoffBalanceOrder
    {
        [JsonPropertyName("trackingId")]
        public string TrackingId { get; set; }
        [JsonPropertyName("payload")]
        public List<TinkoffPayload> Payloads { get; set; }
    }
}
