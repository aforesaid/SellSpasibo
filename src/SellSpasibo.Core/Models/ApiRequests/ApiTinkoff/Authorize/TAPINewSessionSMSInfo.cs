using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.Authorize
{
    public class TAPINewSessionSMSInfo
    {
        [JsonPropertyName("resultCode")]
        public string ResultCode { get; set; }
        [JsonPropertyName("operationTicket")]
        public string OperationTicket { get; set; }
    }
}