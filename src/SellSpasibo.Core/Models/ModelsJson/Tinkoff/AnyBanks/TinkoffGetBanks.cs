using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.AnyBanks
{
    public class TinkoffGetBanks
    {
        [JsonPropertyName("payload")]
        public List<TinkoffBankJson> Payload { get; set; }
        [JsonPropertyName("resultCode")]
        public string ResultCode { get; set; }
    }
}
