using System.Text.Json.Serialization;
using SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.Base;

namespace SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.GetTransactionHistory
{
    public class SAPIGetHistoryResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public SAPIGetTransactionHistoryData Data { get; set; }
    }
}
