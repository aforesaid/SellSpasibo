namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBalance
{
    public class TAPIGetBalanceRequestItem
    {
        public TAPIGetBalanceRequestItem()
        {
            Params = new TAPIGetBalanceParamsJson();
        }
        public int Key { get; } = 0;
        public string Operation { get; } = "accounts_flat";
        public TAPIGetBalanceParamsJson Params { get; }

        public TAPIGetBalanceRequestItem[] ToRequest()
        {
            return new [] {this};
        }
    }

    public class TAPIGetBalanceParamsJson
    {
        public TAPIGetBalanceParamsJson()
        {
        }
        public string Wuid { get; }
    }
}