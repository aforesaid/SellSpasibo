using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Balance
{
    class SberSpasiboLoyaltySystemJson
    {
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
