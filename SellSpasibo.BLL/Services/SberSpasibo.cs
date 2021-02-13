using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SellSpasibo.BLL.Interfaces;

namespace SellSpasibo.BLL.Services
{
    class SberSpasibo : ISberSpasibo
    {
        private const string Domain = "https://www.tinkoff.ru/api/online";

        private string _authToken = null;
        public async Task<bool> UpdateSession()
        {
            using var client = new HttpClient();
            var       link   = $"{Domain}/auth/refresh";
            var response = await client.GetAsync(link);
            //TODO: добавить обновление токена
            if (response.StatusCode != HttpStatusCode.OK)
                //TODO: добавить логику логгирования ошибки
                return false;
            return true;
        }
    }
}
