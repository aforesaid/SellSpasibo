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

namespace SellSpasibo.BLL.Services
{
    public class SberSpasibo : ISberSpasibo
    {
        private const string Domain = "https://new.spasibosberbank.ru/api/online";
        private static int DefaultCountTransactionByQuery { get; } = 500;

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
            var link     = $"{Domain}/auth/refresh";
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
            var       link   = $"{Domain}/personal/loyalitySystem/transactions?page=1&cnt={DefaultCountTransactionByQuery}";
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
            var response = await client.GetAsync(link);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SberSpasiboGetHistoryJson>(contentString);
        }

        public async Task<SberSpasiboNewOrderJson> CreateNewOrder(string cost, string number)
        {
            using var client = new HttpClient();
            var       link   = $"{Domain}/personal/loyalitySystem/convert";
            var content =
                new
                    StringContent($"{{\"converterId\":\"p_2_p\",\"sumToConvert\":{cost},\"cardId\":\"\",\"phone\":\"{number}\",\"number\":\"\"}}",
                                  Encoding.UTF8, "application/json");
            var response = await client.PostAsync(link, content);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SberSpasiboNewOrderJson>(contentString);
        }
        public async Task<SberSpasiboGetCurrentBalanceJson> GetBalance()
        {
            using var client = new HttpClient();
            var link = $"{Domain}/personal/me";
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
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
            var       link   = $"{Domain}/personal/loyalitySystem/converter/checkToClient";
            var content =
                new
                    StringContent($"{{\"sumToConvert\":1,\"phone\":\"{phone}\",\"converterId\":\"p_2_p\"}}",
                                  Encoding.UTF8, "application/json");
            var response = await client.PostAsync(link, content);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            //TODO: добавить проверку на выполнение платежа
            return JsonSerializer.Deserialize<SberSpasiboCheckClientJson>(contentString);
        }
    }
}