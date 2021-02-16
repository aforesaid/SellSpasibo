using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Balance
{
    class SberSpasiboDataJson
    {
        [JsonPropertyName("loyaltySystem")]
        public SberSpasiboLoyaltySystemJson LoyaltySystem { get; set; }
    }
}
