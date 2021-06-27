using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.GetTransactionHistory
{
    public class SAPIGetTransactionHistoryData
    {
        [JsonPropertyName("transactions")]
        public List<SAPITransaction> Transactions { get; set; }
    }
}
