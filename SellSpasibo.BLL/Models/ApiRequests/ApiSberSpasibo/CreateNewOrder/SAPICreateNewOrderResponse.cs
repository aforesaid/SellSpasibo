using System.Text.Json.Serialization;
using SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.Base;

namespace SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.CreateNewOrder
{
    public class SAPICreateNewOrderResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public SAPICreateNewOrderData Data { get; set; }
    }
}
