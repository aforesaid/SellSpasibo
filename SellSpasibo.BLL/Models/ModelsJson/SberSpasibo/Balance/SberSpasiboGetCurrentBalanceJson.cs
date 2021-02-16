using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Balance
{
    class SberSpasiboGetCurrentBalanceJson
    {
        [JsonPropertyName("data")]
        public SberSpasiboDataJson Data { get; set; }
    }
}
