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
using SellSpasibo.BLL.Models.ModelsJson;

namespace SellSpasibo.BLL.Services
{
    public class Tinkoff : ITinkoff
    {
        

        private static string _sessionId;
        private static string _wuId;
        private static string _account;
        public static void SetValue(string sessionId, string wuId,
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
            using var client = new HttpClient();
            var link = UrlsConstants.TinkoffConst.GetInfoByUserLink(number, bankMemberId, _sessionId, _wuId);
            var response = await client.GetAsync(link);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TinkoffCheckUserParams>(contentString);
        }
        public async Task<TinkoffGetBanks> GetBankMember()
        {
            using var client   = new HttpClient();
            var link = UrlsConstants.TinkoffConst.GetBankMemberLink(_sessionId, _wuId);
            var       response = await client.GetAsync(link);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TinkoffGetBanks>(contentString);
        }

        public async Task<TinkoffSendOrderJson> CreateNewOrder(Order order)
        {
            order.Account = _account;

            using var client         = new HttpClient();
            var link = UrlsConstants.TinkoffConst.CreateNewOrderLink(_sessionId, _wuId);
            var content = new Dictionary<string, string>()
            {
                ["payParameters"] = order.ToString()
            };
            var requestContent = new FormUrlEncodedContent(content);
            var response       = await client.PostAsync(link, requestContent);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TinkoffSendOrderJson>(contentString);
        }

        public async Task<TinkoffBalanceOrder> GetBalance()
        {
            using var client         = new HttpClient();
            var content = new Dictionary<string, string>()
            {
                ["requestsData"] = @$"[{{""key"":0,""operation"":""accounts_flat"",""params"":{{""wuid"":""{_wuId}""}}}}]"
            };
            var link = UrlsConstants.TinkoffConst.GetBalanceLink(_sessionId);
            var       requestContent = new FormUrlEncodedContent(content);
            var       response       = await client.PostAsync(link, requestContent);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TinkoffBalanceOrder>(contentString);
        }
    }
}
