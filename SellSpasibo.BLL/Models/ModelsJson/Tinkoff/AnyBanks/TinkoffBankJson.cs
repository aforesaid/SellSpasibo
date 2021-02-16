using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.Tinkoff.AnyBanks
{
    public class TinkoffBankJson
    {
        [JsonPropertyName("bankMemberId")]
        public string BankMemberId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
