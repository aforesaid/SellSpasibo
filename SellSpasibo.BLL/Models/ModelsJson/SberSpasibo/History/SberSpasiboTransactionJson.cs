using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.History
{
    public class SberSpasiboTransactionJson
    {
        [JsonPropertyName("bonusBalanceChange")]
        public double BonusBalanceChange { get; set; }
        [JsonPropertyName("operationDate")]
        public string Date { get; set; }
        [JsonPropertyName("operationTime")]
        public string Time { get; set; }
        [JsonPropertyName("partnerName")]
        public string PartnerName { get; set; }
    }
}
