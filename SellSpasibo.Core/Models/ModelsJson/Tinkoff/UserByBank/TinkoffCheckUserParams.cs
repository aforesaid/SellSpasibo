using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.UserByBank
{
    public class TinkoffCheckUserParams
    {
        [JsonPropertyName("resultCode")]
        public string ResultCode { get; set; }
        [JsonPropertyName("payload")]
        public TinkoffPayloadJson[] Payload { get; set; }
    }
}
