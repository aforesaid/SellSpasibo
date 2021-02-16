using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.History
{
    public class SberSpasiboDataJson
    {
        [JsonPropertyName("transaction")]
        public List<SberSpasiboTransactionJson> Transactions { get; set; }
    }
}
