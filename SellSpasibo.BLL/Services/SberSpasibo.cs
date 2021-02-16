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

namespace SellSpasibo.BLL.Services
{
    public class SberSpasibo : ISberSpasibo
    {
        private const string Domain = "https://www.tinkoff.ru/api/online";
        private static int DefaultCountTransactionByQuery { get; } = 500;

        private static string _authToken;
        public static void SetValue(string authToken)
        {
            _authToken = authToken;
        }

        public async Task<bool> UpdateSession()
        {
            using var client   = new HttpClient();
            var       link     = $"{Domain}/auth/refresh";
            var       response = await client.GetAsync(link);
            //TODO: добавить обновление токена
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return false;
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