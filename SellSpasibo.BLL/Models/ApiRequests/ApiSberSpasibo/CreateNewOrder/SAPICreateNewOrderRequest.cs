namespace SellSpasibo.BLL.Models.ApiRequests.ApiSberSpasibo.CreateNewOrder
{
    public class SAPICreateNewOrderRequest
    {
        public SAPICreateNewOrderRequest(double cost, string phoneNumber)
        {
            SumToConvert = cost;
            Phone = phoneNumber;
        }
        public double SumToConvert { get; }
        public string Phone { get; }
        
        public string ConverterId { get; } = "p_2_p";
        public string CardId { get; } = @"\";
        public string Number { get; } = @"\";
        
    }
}