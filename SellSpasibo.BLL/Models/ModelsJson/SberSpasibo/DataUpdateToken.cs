using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo
{
    public class DataUpdateToken
    {
        [JsonPropertyName("data")]
        public UpdateToken Info { get; set; }
    }
    public class UpdateToken
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
