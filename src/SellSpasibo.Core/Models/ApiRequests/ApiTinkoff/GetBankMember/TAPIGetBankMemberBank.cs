using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBankMember
{
    public class TAPIGetBankMemberBank
    {
        [JsonPropertyName("bankMemberId")]
        public string BankMemberId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
