using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff
{
    public class TAPITinkoffCurrency
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
