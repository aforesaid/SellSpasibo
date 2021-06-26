using SellSpasibo.BLL.Interfaces;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Balance;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.CheckClient;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.History;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.NewOrder;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Requests;

namespace SellSpasibo.BLL.Services
{
    public class SberSpasibo : ISberSpasibo
    {
        private static string _authToken;
        private static string _refreshToken;
        public static void SetValue(string authToken,
            string refreshToken)
        {
            _authToken = authToken;
            _refreshToken = refreshToken;
        }

        public async Task<bool> UpdateSession()
        {
            using var client   = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
            var link = UrlsConstants.SberConst.UpdateSessionLink;
            var content = new StringContent($"{{\"refreshToken\":\"{_refreshToken}\"}}",Encoding.UTF8, "application/json");
            var       response = await client.PostAsync(link,content);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return false;
            var stringContent = await response.Content.ReadAsStringAsync();
            var info = JsonSerializer.Deserialize<DataUpdateToken>(stringContent);
            (_refreshToken, _authToken) = (info.Info.RefreshToken, info.Info.Token);
            return true;
        }

        public async Task<SberSpasiboGetHistoryJson> GetTransactionHistory()
        {
            using var client = new HttpClient();
            //TODO: добавить возможность прогружать всю историю
            var link = UrlsConstants.SberConst.GetTransactionHistoryLink(1);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
            var response = await client.GetAsync(link);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SberSpasiboGetHistoryJson>(contentString);
        }

        public async Task<SberSpasiboNewOrderJson> CreateNewOrder(double cost, string number)
        {
            using var client = new HttpClient();
            var request = new CreateNewOrderRequestJson(cost, number);
            var link = UrlsConstants.SberConst.CreateNewOrderLink;
            var response = await client.PostAsync(link, request.ToRequest());
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SberSpasiboNewOrderJson>(contentString);
        }
        public async Task<SberSpasiboGetCurrentBalanceJson> GetBalance()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
            var link = UrlsConstants.SberConst.GetBalanceLink;
            var response = await client.GetAsync(link);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SberSpasiboGetCurrentBalanceJson>(contentString);
        }
        public async Task<SberSpasiboCheckClientJson> CheckClient(string phone)
        {
            using var client = new HttpClient();
            var request = new CheckClientRequestJson(phone);
            var link = UrlsConstants.SberConst.CheckClientLink;
            var response = await client.PostAsync(link, request.ToRequest());
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            //TODO: добавить проверку на выполнение платежа
            return JsonSerializer.Deserialize<SberSpasiboCheckClientJson>(contentString);
        }
    }
}