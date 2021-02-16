using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.NewOrder
{
    class SberSpasiboDataJson
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}
