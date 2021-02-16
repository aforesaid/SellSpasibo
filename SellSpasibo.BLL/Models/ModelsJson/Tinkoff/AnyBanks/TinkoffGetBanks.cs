using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.Tinkoff.AnyBanks
{
    public class TinkoffGetBanks
    {
        [JsonPropertyName("Payload")]
        public List<TinkoffBankJson> Payload { get; set; }
        [JsonPropertyName("resultCode")]
        public string ResultCode { get; set; }
    }
}
