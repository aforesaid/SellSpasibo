using System;
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
using Microsoft.Extensions.Logging;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Requests;

namespace SellSpasibo.BLL.Services
{
    public class SberSpasibo : ISberSpasibo
    {
        private string _authToken;
        private string _refreshToken;

        private readonly ILogger<SberSpasibo> _logger;

        public SberSpasibo(ILogger<SberSpasibo> logger)
        {
            _logger = logger;
        }

        private HttpClient _httpClient = new HttpClient();
        public void SetTokens(string authToken,
            string refreshToken)
        {
            _authToken = authToken;
            _refreshToken = refreshToken;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
        }

        public async Task<bool> UpdateSession()
        {
            var link = UrlsConstants.SberConst.UpdateSessionLink;
            var request = new UpdateSessionRequestJson(_refreshToken);
            var       response = await _httpClient.PostAsync(link,request.ToRequest());
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return false;
            var stringContent = await response.Content.ReadAsStringAsync();
            var info = JsonSerializer.Deserialize<DataUpdateToken>(stringContent);
            SetTokens(info.Info.Token, info.Info.RefreshToken);
            return true;
        }

        public async Task<SberSpasiboGetHistoryJson> GetTransactionHistory()
        {
            //TODO: добавить возможность прогружать всю историю
            var link = UrlsConstants.SberConst.GetTransactionHistoryLink(1);
            var response = await _httpClient.GetAsync(link);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SberSpasiboGetHistoryJson>(contentString);
        }

        public async Task<SberSpasiboNewOrderJson> CreateNewOrder(double cost, string number)
        {
            var request = new CreateNewOrderRequestJson(cost, number);
            var link = UrlsConstants.SberConst.CreateNewOrderLink;
            var response = await _httpClient.PostAsync(link, request.ToRequest());
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SberSpasiboNewOrderJson>(contentString);
        }
        public async Task<SberSpasiboGetCurrentBalanceJson> GetBalance()
        {
            var link = UrlsConstants.SberConst.GetBalanceLink;
            var response = await _httpClient.GetAsync(link);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SberSpasiboGetCurrentBalanceJson>(contentString);
        }
        public async Task<SberSpasiboCheckClientJson> CheckClient(string phone)
        {
            var request = new CheckClientRequestJson(phone);
            var link = UrlsConstants.SberConst.CheckClientLink;
            var response = await _httpClient.PostAsync(link, request.ToRequest());
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            //TODO: добавить проверку на выполнение платежа
            return JsonSerializer.Deserialize<SberSpasiboCheckClientJson>(contentString);
        }

        private async Task<TResponse> Post<TRequest, TResponse>(string url, TRequest request)
        where TRequest: class
        {
            try
            {
                var contentString = JsonSerializer.Serialize(request);
                var content = new StringContent(contentString, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TResponse>(responseString);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError("Не удалось выполнить запрос к url :{0}, request : {@1}",
                    url, request);
                throw;
            }
        }
    }
}