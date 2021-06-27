using System.Text.Json.Serialization;
using SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.Base;

namespace SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.GetBalance
{
    public class SAPIGetCurrentBalanceResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public SAPIGetBalanceData Data { get; set; }
    }
}
