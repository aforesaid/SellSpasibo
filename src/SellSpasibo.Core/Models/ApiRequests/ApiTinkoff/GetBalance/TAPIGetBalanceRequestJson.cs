namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.Requests
{
    public class TAPIGetBalanceRequestJson
    {
        public TAPIGetBalanceRequestJson(string wuid)
        {
            Params = new GetBalanceParamsJson(wuid);
        }
        public int Key { get; } = 0;
        public string Operation { get; } = "accounts_flat";
        public GetBalanceParamsJson Params { get; }

        public TAPIGetBalanceRequestJson[] ToRequest()
        {
            return new [] {this};
        }
    }

    public class GetBalanceParamsJson
    {
        public GetBalanceParamsJson(string wuid)
        {
            Wuid = wuid;
        }
        public string Wuid { get; }
    }
}