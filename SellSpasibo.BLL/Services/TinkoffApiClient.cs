using System;
using SellSpasibo.BLL.Interfaces;
using SellSpasibo.BLL.Models.ModelsJson.Tinkoff.AnyBanks;
using SellSpasibo.BLL.Models.ModelsJson.Tinkoff.Balance;
using SellSpasibo.BLL.Models.ModelsJson.Tinkoff.NewOrder;
using SellSpasibo.BLL.Models.ModelsJson.Tinkoff.UserByBank;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SellSpasibo.BLL.Models.ModelsJson;
using SellSpasibo.BLL.Models.ModelsJson.Tinkoff.Requests;
using SellSpasibo.BLL.Options;

namespace SellSpasibo.BLL.Services
{
    public class TinkoffApiClient : ITinkoff
    {
        private  string _sessionId;
        private  string _wuId;
        private  string _account;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly ILogger<TinkoffApiClient> _logger;

        public TinkoffApiClient(ILogger<TinkoffApiClient> logger,
            IOptions<TinkoffOptions> options)
        {
            _logger = logger;
            SetTokens(options.Value.SessionId, options.Value.WuId, options.Value.Account);
        }

        public void SetTokens(string sessionId, string wuId,
            string account)
        {
            _sessionId = sessionId;
            _wuId      = wuId;
            _account   = account;
        }
        public async Task<bool> UpdateSession()
        {
            using var client   = new HttpClient();
            var link = UrlsConstants.TinkoffConst.UpdateSessionLink(_sessionId, _wuId);
            var       response = await client.GetAsync(link);
            return response.StatusCode == HttpStatusCode.OK;
        }
        public async Task<TinkoffCheckUserParams> GetInfoByUser(string number, string bankMemberId)
        {
            var link = UrlsConstants.TinkoffConst.GetInfoByUserLink(number, bankMemberId, _sessionId, _wuId);
            var response = await GetAsync<TinkoffCheckUserParams>(link);
            return response;
        }
        public async Task<TinkoffGetBanks> GetBankMember()
        {
            var link = UrlsConstants.TinkoffConst.GetBankMemberLink(_sessionId, _wuId);
            var       response = await GetAsync<TinkoffGetBanks>(link);
            return response;
        }

        public async Task<TinkoffSendOrderJson> CreateNewOrder(Order order)
        {
            order.Account = _account;
            
            var link = UrlsConstants.TinkoffConst.CreateNewOrderLink(_sessionId, _wuId);
            const string columnName = "payParameters";
            
            var response = await PostAsync<Order,TinkoffSendOrderJson>(link, columnName, order);
            return response;
        }

        public async Task<TinkoffBalanceOrder> GetBalance()
        {
            var link = UrlsConstants.TinkoffConst.GetBalanceLink(_sessionId);
            const string columnName = "requestsData";
            var request = new GetBalanceRequestJson(_wuId);
            var response = await PostAsync<GetBalanceRequestJson[], TinkoffBalanceOrder>(link, columnName, request.ToRequest());
            return response;
        }
        private async Task<TResponse> PostAsync<TRequest, TResponse>(string url, string columnName,
            TRequest request)
            where TRequest: class
        {
            try
            {
                var contentString = JsonSerializer.Serialize(request);
                var contentDictionary = new Dictionary<string, string>()
                {
                    [columnName] = contentString
                };
                var content = new FormUrlEncodedContent(contentDictionary);

                var response = await _httpClient.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TResponse>(responseString);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Не удалось выполнить post-запрос к url :{0},columnName : {1} request : {@2}",
                    url, columnName, request);
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
