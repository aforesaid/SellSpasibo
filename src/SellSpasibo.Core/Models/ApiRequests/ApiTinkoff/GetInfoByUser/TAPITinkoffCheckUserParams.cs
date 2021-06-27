using System.Text.Json.Serialization;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetInfoByUser;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.UserByBank
{
    public class TAPITinkoffCheckUserParams
    {
        [JsonPropertyName("resultCode")]
        public string ResultCode { get; set; }
        [JsonPropertyName("payload")]
        public TAPITinkoffPayloadJson[] Payload { get; set; }
    }
}
