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
        
    }
}