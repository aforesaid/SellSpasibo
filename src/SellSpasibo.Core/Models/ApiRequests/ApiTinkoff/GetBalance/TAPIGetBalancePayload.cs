using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBalance
{
    public class TAPIGetBalancePayload
    {
        [JsonPropertyName("payload")]
        public List<TAPIGetBalanceInfoByCard> Cards { get; set; }
        [JsonPropertyName("resultCode")]
        public string ResultCode { get; set; }
    }

    public class TAPIGetBalancePayloadInfo
    {
        //TODO: пересмотреть
        [JsonPropertyName("0")]
        public TAPIGetBalancePayload Payload { get; set; }
    }
}
