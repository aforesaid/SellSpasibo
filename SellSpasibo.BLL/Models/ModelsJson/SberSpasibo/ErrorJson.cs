using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo
{
    public class ErrorJson
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("messages")]
        public string[] Messages { get; set; }
    }
}