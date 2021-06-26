using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Requests
{
    public class UpdateSessionRequestJson
    {
        public UpdateSessionRequestJson(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
        public string RefreshToken { get; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public StringContent ToRequest()
        {
            return new StringContent(ToString(), Encoding.UTF8, "application/json");
        }
    }
}