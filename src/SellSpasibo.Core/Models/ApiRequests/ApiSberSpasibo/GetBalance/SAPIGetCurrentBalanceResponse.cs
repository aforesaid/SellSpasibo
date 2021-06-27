using System.Text.Json.Serialization;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.Base;

namespace SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.GetBalance
{
    public class SAPIGetCurrentBalanceResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public SAPIGetBalanceData Data { get; set; }
    }
}
