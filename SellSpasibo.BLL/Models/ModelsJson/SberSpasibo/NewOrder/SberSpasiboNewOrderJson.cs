using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.NewOrder
{
    class SberSpasiboNewOrderJson
    {
        [JsonPropertyName("data")]
        public SberSpasiboDataJson Data { get; set; }
    }
}
