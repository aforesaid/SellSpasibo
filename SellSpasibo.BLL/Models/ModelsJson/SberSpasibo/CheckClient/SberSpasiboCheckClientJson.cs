using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.CheckClient
{
    class SberSpasiboCheckClientJson
    {
        [JsonPropertyName("data")]
        public SberSpasiboDataJson Data { get; set; }
    }
}
