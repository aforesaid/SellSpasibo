using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.History
{
    class SberSpasiboGetHistoryJson
    {
        [JsonPropertyName("data")]
        public SberSpasiboDataJson Data { get; set; }
    }
}
