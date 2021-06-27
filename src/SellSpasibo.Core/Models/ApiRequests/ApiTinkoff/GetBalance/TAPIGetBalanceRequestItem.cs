namespace SellSpasibo.Core.Models.ApiRequests.ApiTinkoff.GetBalance
{
    public class TAPIGetBalanceRequestItem
    {
        public TAPIGetBalanceRequestItem(string wuid)
        {
            Params = new TAPIGetBalanceParamsJson(wuid);
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
        public TAPIGetBalanceParamsJson(string wuid)
        {
            Wuid = wuid;
        }
        public string Wuid { get; }
    }
}