using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.CheckClient
{
    public class SberSpasiboDataJson
    {
        [JsonPropertyName("maxSum")]
        public decimal MaxSum { get; set; }
        [JsonPropertyName("minSum")]
        public decimal MinSum { get; set; }
    }
}
