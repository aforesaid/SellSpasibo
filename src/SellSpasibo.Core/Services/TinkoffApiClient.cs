using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SellSpasibo.Core.Interfaces;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.Authorize;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.CreateNewOrder;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBalance;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBankMember;
using SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetInfoByUser;
using SellSpasibo.Core.Models.Authorization.Tinkoff;
using SellSpasibo.Core.Options;

namespace SellSpasibo.Core.Services
{
    public class TinkoffApiClient : ITinkoffApiClient
    {
        private readonly HttpClient _httpClient = new();
        private readonly ILogger<TinkoffApiClient> _logger;

        public TinkoffApiClient(ILogger<TinkoffApiClient> logger)
        {
            _logger = logger;
        }

        public async Task<TinkoffNewSessionInfo> SendSms(string login)
        {
            var linkCreateSession = UrlsConstants.TinkoffConst.CreateSessionLink;
            var createSession = await GetAsync<TAPICreateSession>(linkCreateSession);

            var sessionId = createSession.SessionId;
            var authorizeLink = UrlsConstants.TinkoffConst.AuthorizeLink(sessionId);
            var request = new Dictionary<string, string>
            {
                ["phone"]= login
            };
            var response = await PostAsync<TAPINewSessionSMSInfo>(authorizeLink, request);

            const string successResultCode = "WAITING_CONFIRMATION";
            if (response.ResultCode == successResultCode)
            {
                return new TinkoffNewSessionInfo(sessionId, response.OperationTicket);
            }
            return null;
        }

        public async Task<bool> Authorize(string sessionId, string password, string operationTicket,
            string code)
        {
            var authorizeLink = UrlsConstants.TinkoffConst.AuthorizeLink(sessionId);
            var request = new Dictionary<string, string>
            {
                ["initialOperation"] = "sign_up",
                ["initialOperationTicket"]= operationTicket,
                ["configrmationData"]=$@"{{""SMSBYID"":""{code}"""
            };
            var response = await PostAsync<TAPISendSMSResponse>(authorizeLink, request);
            const string successResultCode = "OK";
            
            if (response.ResultCode != successResultCode)
                return false;

            request = new Dictionary<string, string>
            {
                ["password"] = password
            };
            response = await PostAsync<TAPISendSMSResponse>(authorizeLink, request);

            return response.ResultCode == successResultCode;
        }
        
        public async Task<bool> UpdateSession(string sessionId)
        {
            using var client   = new HttpClient();
            var link = UrlsConstants.TinkoffConst.UpdateSessionLink(sessionId);
            var       response = await client.GetAsync(link);
            return response.StatusCode == HttpStatusCode.OK;
        }
        public async Task<TAPIGetInfoByUserPayload> GetInfoByUser(string sessionId, string number)
        {
            var linkInternal = UrlsConstants.TinkoffConst.GetInfoByUserInternalLink(number, sessionId);

            var responseInternal = await GetAsync<TAPIGetInfoByUserResponse>(linkInternal);
            
            return responseInternal.Payload?.FirstOrDefault();
        }
        public async Task<TAPIGetBankMemberResponse> GetBankMember(string sessionId)
        {
            var link = UrlsConstants.TinkoffConst.GetBankMemberLink(sessionId);
            var       response = await GetAsync<TAPIGetBankMemberResponse>(link);
            return response;
        }

        public async Task<TAPICreateNewOrderResponse> CreateNewOrder(string
             sessionId, TAPICreateNewOrderRequest order)
        {
            var link = UrlsConstants.TinkoffConst.CreateNewOrderLink(sessionId);
            const string columnName = "payParameters";
            
            var response = await PostAsync<TAPICreateNewOrderRequest,TAPICreateNewOrderResponse>(link, columnName, order);
            return response;
        }

        public async Task<TAPIGetBalanceResponse> GetBalance(string sessionId)
        {
            var link = UrlsConstants.TinkoffConst.GetBalanceLink(sessionId);
            const string columnName = "requestsData";
            var request = new TAPIGetBalanceRequestItem();
            var response = await PostAsync<TAPIGetBalanceRequestItem[], TAPIGetBalanceResponse>(link, columnName, request.ToRequest());
            return response;
        }
        private async Task<TResponse> PostAsync<TRequest, TResponse>(string url, string columnName,
            TRequest request)
            where TRequest: class
        {
            try
            {
                var contentString = JsonSerializer.Serialize(request, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                });
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
        private async Task<TResponse> PostAsync<TResponse>(string url,
            Dictionary<string,string> request)
        {
            try
            {
                var content = new FormUrlEncodedContent(request);

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
