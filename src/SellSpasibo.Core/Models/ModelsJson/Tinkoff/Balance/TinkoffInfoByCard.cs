using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.Balance
{
    public class TinkoffInfoByCard
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("accountBalance")]
        public TinkoffAccountBalance AccountBalance { get; set; }
    }
}
