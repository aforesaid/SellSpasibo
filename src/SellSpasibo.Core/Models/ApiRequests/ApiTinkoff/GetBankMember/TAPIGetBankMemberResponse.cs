using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBankMember
{
    public class TAPIGetBankMemberResponse
    {
        [JsonPropertyName("payload")]
        public List<TAPIGetBankMemberBank> Payload { get; set; }
        [JsonPropertyName("resultCode")]
        public string ResultCode { get; set; }
    }
}
