using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.CheckClient
{
    public class SberSpasiboCheckClientJson
    {
        [JsonPropertyName("data")]
        public SberSpasiboDataJson Data { get; set; }
        [JsonPropertyName("error")]
        public ErrorJson Error { get; set; }
    }
}
