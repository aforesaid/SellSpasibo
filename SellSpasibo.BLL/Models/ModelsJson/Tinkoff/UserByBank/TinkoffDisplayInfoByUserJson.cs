using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.Tinkoff.UserByBank
{
    public class TinkoffDisplayInfoByUserJson
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
