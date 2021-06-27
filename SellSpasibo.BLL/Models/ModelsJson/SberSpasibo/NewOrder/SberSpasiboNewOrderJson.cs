using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.NewOrder
{
    public class SberSpasiboNewOrderJson
    {
        [JsonPropertyName("data")]
        public SberSpasiboDataJson Data { get; set; }
        [JsonPropertyName("error")]
        public ErrorJson Error { get; set; }
    }
}
