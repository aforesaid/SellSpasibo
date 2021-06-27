using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.History
{
    public class SberSpasiboDataJson
    {
        [JsonPropertyName("transactions")]
        public List<SberSpasiboTransactionJson> Transactions { get; set; }
    }
}
