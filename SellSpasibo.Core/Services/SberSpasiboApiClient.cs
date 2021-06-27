using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.CheckClient;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.CreateNewOrder;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.GetBalance;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.GetTransactionHistory;
using SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.UpdateSession;
using SellSpasibo.Core.Options;

namespace SellSpasibo.Core.Services
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
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
        }

        public async Task<bool> UpdateSession()
        {
            var link = UrlsConstants.SberConst.UpdateSessionLink;
            var request = new SAPIUpdateSessionRequest(_refreshToken);
            var response = await PostAsync<SAPIUpdateSessionRequest, SAPIUpdateSessionResponse>(link, request);
            if (response.IsSuccess)
            {
                SetTokens(response.Info.Token, response.Info.RefreshToken);
            }
            return response.IsSuccess;
        }

        public async Task<SAPITransaction[]> GetTransactionHistory()
        {
            var transactionList = new List<SAPITransaction>();
            var counter = 1;
            const int countItems = 500;
            const string partnerName = "Перевод от участника";
            SAPITransaction[] items;
            do
            {
                var link = UrlsConstants.SberConst.GetTransactionHistoryLink(counter, countItems);
                var response = await GetAsync<SAPIGetHistoryResponse>(link);
                items = response?.Data?.Transactions?.ToArray();
                
                if (items != null)
                {
                    transactionList.AddRange(items
                        .Where(x => x.PartnerName == partnerName));
                    counter++;
                }
                
            } while (items?.Length == countItems);

            return transactionList.ToArray();
        }

        public async Task<SAPICreateNewOrderResponse> CreateNewOrder(double cost, string number)
        {
            var request = new SAPICreateNewOrderRequest(cost, number);
            var link = UrlsConstants.SberConst.CreateNewOrderLink;
            var response = await PostAsync<SAPICreateNewOrderRequest, SAPICreateNewOrderResponse>(link, request);
            return response;
        }

        public async Task<SAPIGetCurrentBalanceResponse> GetBalance()
        {
            var link = UrlsConstants.SberConst.GetBalanceLink;
            var response = await GetAsync<SAPIGetCurrentBalanceResponse>(link);
            return response;
        }
        public async Task<SAPICheckClientResponse> CheckClient(string phone)
        {
            var request = new SAPICheckClientRequest(phone);
            var link = UrlsConstants.SberConst.CheckClientLink;
            var response = await PostAsync<SAPICheckClientRequest, SAPICheckClientResponse>(link, request);
            return response;
        }

        private async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request)
        where TRequest: class
        {
            try
            {
                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var contentString = JsonSerializer.Serialize(request, jsonSerializerOptions);
                var content = new StringContent(contentString, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TResponse>(responseString, jsonSerializerOptions);
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
                var result = JsonSerializer.Deserialize<TResponse>(responseString, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
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