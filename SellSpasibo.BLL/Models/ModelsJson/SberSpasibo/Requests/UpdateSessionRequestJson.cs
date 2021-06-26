namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Requests
{
    public class UpdateSessionRequestJson
    {
        public UpdateSessionRequestJson(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
        public string RefreshToken { get; }
    }
}