using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Balance
{
    public class SberSpasiboDataJson
    {
        [JsonPropertyName("loyaltySystem")]
        public SberSpasiboLoyaltySystemJson LoyaltySystem { get; set; }
    }
}
