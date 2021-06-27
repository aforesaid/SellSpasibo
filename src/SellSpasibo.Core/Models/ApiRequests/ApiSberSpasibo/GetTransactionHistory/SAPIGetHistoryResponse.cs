using System.Text.Json.Serialization;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.Base;

namespace SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.GetTransactionHistory
{
    public class SAPIGetHistoryResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public SAPIGetTransactionHistoryData Data { get; set; }
    }
}
