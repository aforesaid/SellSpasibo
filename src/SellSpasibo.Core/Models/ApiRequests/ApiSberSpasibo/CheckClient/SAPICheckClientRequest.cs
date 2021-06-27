namespace SellSpasibo.Core.Models.ApiRequests.ApiSberSpasibo.CheckClient
{
    public class SAPICheckClientRequest
    {
        public SAPICheckClientRequest(string phoneNumber)
        {
            Phone = phoneNumber;
        }
        public string Phone { get; }
        
        public int SumToConvert { get; } = 1;
        public string ConverterId { get; } = "p_2_p";
        
    }
}