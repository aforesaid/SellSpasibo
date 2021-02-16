using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Balance
{
    public class SberSpasiboLoyaltySystemJson
    {
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
