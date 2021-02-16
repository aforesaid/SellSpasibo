using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.Tinkoff.Balance
{
    public class TinkoffInfoByCard
    {
        [JsonPropertyName("accountBalance")]
        public TinkoffAccountBalance AccountBalance { get; set; }
    }
}
