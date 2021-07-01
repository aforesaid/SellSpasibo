using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.Authorize
{
    public class TAPISendSMSResponse
    {
        [JsonPropertyName("resultCode")]
        public string ResultCode { get; set; }
    }
}