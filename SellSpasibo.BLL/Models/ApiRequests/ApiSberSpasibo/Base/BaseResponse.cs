using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.Base
{
    public abstract class BaseResponse
    {
        [JsonPropertyName("error")]
        public ApiError Error { get; set; }

        public bool IsSuccess => Error == null;

    }
}