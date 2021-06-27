using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.Balance
{
    public class TAPITinkoffInfoByCard
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("accountBalance")]
        public TAPITinkoffAccountBalance AccountBalance { get; set; }
    }
}
