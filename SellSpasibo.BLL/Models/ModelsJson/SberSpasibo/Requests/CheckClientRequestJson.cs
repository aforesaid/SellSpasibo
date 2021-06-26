using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Requests
{
    public class CheckClientRequestJson
    {
        public CheckClientRequestJson(string phoneNumber)
        {
            Phone = phoneNumber;
        }
        public string Phone { get; }
        
        public int SumToConvert { get; } = 1;
        public string ConverterId { get; } = "p_2_p";
        
        public StringContent ToRequest()
        {
            return new StringContent(JsonSerializer.Serialize(this), Encoding.UTF8, "application/json");
        }
    }
}