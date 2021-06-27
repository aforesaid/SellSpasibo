using System.Text.Json.Serialization;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.Base;

namespace SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.CreateNewOrder
{
    public class SAPICreateNewOrderResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public SAPICreateNewOrderData Data { get; set; }
    }
}
