using SellSpasibo.BLL.Interfaces;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SellSpasibo.BLL.Models;

namespace SellSpasibo.BLL.Services
{
    public class Tinkoff : ITinkoff
    {
        private const string Domain = "https://www.tinkoff.ru/api/common";
        private const string VersionApi = "v1";
        private const string DefaultLinkByPing = "ping?appName=payments&appVersion=2.6.3&origin=web%2Cib5%2Cplatform";
        private const string DefaultLinkByGetInfoByUser = "get_requisites?pointerType=phone&pointerSource=sbp";

        private string _sessionId = null;
        private string _wuId = null;
        public void SetValue(string sessionId, string wuId)
        {
            _sessionId = sessionId;
            _wuId      = wuId;
        }
        public async Task<bool> UpdateSession()
        {
            using var client   = new HttpClient();
            var       link     = $"{Domain}/{VersionApi}/{DefaultLinkByPing}&sessionid={_sessionId}&wuid={_wuId}";
            var       response = await client.GetAsync(link);
            return response.StatusCode == HttpStatusCode.OK;
        }
        public async Task<string> GetInfoByUser(string number, string bankMemberId)
        {
            using var client = new HttpClient();
            var link =
                $"{Domain}/{VersionApi}/{DefaultLinkByGetInfoByUser}&pointer=%2B{number}&bankMemberId={bankMemberId}&sessionid={_sessionId}&wuid={_wuId}";
            var response = await client.GetAsync(link);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            //TODO : добавить перевод в объект из json
            return contentString;
        }
        public async Task<string> GetBankMember()
        {
            using var client   = new HttpClient();
            var       link     = $"{Domain}/{VersionApi}/sbp_dictionary?sessionid={_sessionId}&wuid={_wuId}";
            var       response = await client.GetAsync(link);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            //TODO : добавить перевод в объект из json
            return contentString;
        }

        public async Task<string> CreateNewOrder(Order order)
        {
            using var client = new HttpClient();
            var       link   = $"{Domain}/{VersionApi}/pay?appName=payments&sessionid={_sessionId}&wuid={_wuId}";
            var       requestContent = new StringContent(order.ToString(), Encoding.UTF8, "application/json");
            var       response = await client.PostAsync(link,requestContent);
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return null;
            var contentString = await response.Content.ReadAsStringAsync();
            //TODO : добавить перевод в объект из json
            return contentString;
        }
    }
}
