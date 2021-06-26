namespace SellSpasibo.BLL.Models.ModelsJson.SberSpasibo.Requests
{
    public class CreateNewOrderRequestJson
    {
        public CreateNewOrderRequestJson(double cost, string phoneNumber)
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