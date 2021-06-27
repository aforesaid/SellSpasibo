using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.CreateNewOrder
{
    public class SAPICreateNewOrderData
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}
