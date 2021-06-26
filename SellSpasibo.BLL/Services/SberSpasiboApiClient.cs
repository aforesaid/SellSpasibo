using System;
using SellSpasibo.BLL.Interfaces;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Balance;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.CheckClient;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.History;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.NewOrder;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo;
using SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Requests;
using SellSpasibo.BLL.Options;

namespace SellSpasibo.BLL.Services
{
    public class SberSpasiboApiClient : ISberSpasibo
    {
        private string _authToken;
        private string _refreshToken;
        private readonly HttpClient _httpClient = new HttpClient();

        private readonly ILogger<SberSpasiboApiClient> _logger;

        public SberSpasiboApiClient(ILogger<SberSpasiboApiClient> logger,
            IOptions<SberOptions> options)
        {
            _logger = logger;
            SetTokens(options.Value.AuthToken, options.Value.RefreshToken);
        }

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
            var response = await PostAsync<UpdateSessionRequestJson, DataUpdateToken>(link, request);
            SetTokens(response.Info.Token, response.Info.RefreshToken);
            return true;
        }

        public async Task<SberSpasiboGetHistoryJson> GetTransactionHistory()
        {
            //TODO: добавить возможность прогружать всю историю
            var link = UrlsConstants.SberConst.GetTransactionHistoryLink(1);
            var response = await GetAsync<SberSpasiboGetHistoryJson>(link);
            return response;
        }

        public async Task<SberSpasiboNewOrderJson> CreateNewOrder(double cost, string number)
        {
            var request = new CreateNewOrderRequestJson(cost, number);
            var link = UrlsConstants.SberConst.CreateNewOrderLink;
            var response = await PostAsync<CreateNewOrderRequestJson, SberSpasiboNewOrderJson>(link, request);
            return response;
        }

        public async Task<SberSpasiboGetCurrentBalanceJson> GetBalance()
        {
            var link = UrlsConstants.SberConst.GetBalanceLink;
            var response = await GetAsync<SberSpasiboGetCurrentBalanceJson>(link);
            return response;
        }
        public async Task<SberSpasiboCheckClientJson> CheckClient(string phone)
        {
            var request = new CheckClientRequestJson(phone);
            var link = UrlsConstants.SberConst.CheckClientLink;
            var response = await PostAsync<CheckClientRequestJson, SberSpasiboCheckClientJson>(link, request);
            return response;
        }

        private async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request)
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
                _logger.LogError(e,"Не удалось выполнить post-запрос к url :{0}, request : {@1}",
                    url, request);
                throw;
            }
        }
        private async Task<TResponse> GetAsync<TResponse>(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TResponse>(responseString);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Не удалось выполнить get-запрос к url :{0}, request : {@1}",
                    url);
                throw;
            }
        }
    }
}