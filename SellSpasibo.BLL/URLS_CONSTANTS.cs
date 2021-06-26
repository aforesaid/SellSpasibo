namespace SellSpasibo.BLL
{
    public static class UrlsConstants
    {
        public static class SberConst
        {
            private const string Domain = "https://new.spasibosberbank.ru/api/online";
            private const int DefaultCountTransactionByQuery = 500;

            public static string UpdateSessionLink = $"{Domain}/auth/refresh";
            public static string CreateNewOrderLink = $"{Domain}/personal/loyalitySystem/convert";
            public static string GetBalanceLink = $"{Domain}/personal/me";
            public static string CheckClientLink = $"{Domain}/personal/loyalitySystem/converter/checkToClient";


            public static string GetTransactionHistoryLink(int page)
            {
                return $"{Domain}/personal/loyalitySystem/transactions?page={page}&cnt={DefaultCountTransactionByQuery}";
            }
        }
        public static class TinkoffConst
        {
            private const string Domain = "https://www.tinkoff.ru/api/common";
            private const string VersionApi = "v1";
            private const string DefaultLinkByPing = "ping?appName=payments&appVersion=2.6.3&origin=web%2Cib5%2Cplatform";
            private const string DefaultLinkByGetInfoByUser = "get_requisites?pointerType=phone&pointerSource=sbp";

            public static string UpdateSessionLink(string sessionId, string wuId)
            {
                return $"{Domain}/{VersionApi}/{DefaultLinkByPing}&sessionid={sessionId}&wuid={wuId}";
            }

            public static string GetInfoByUserInternalLink(string number, string sessionId, string wuId)
            {
                return $"{Domain}/{VersionApi}/{DefaultLinkByGetInfoByUser}&pointer=%2B{number}&pointerSource=internal&pointerType=phone&sessionid={sessionId}&wuid={wuId}";
            }
            //Поддерживаются пока только банк тинькофф
            public static string GetInfoByUserExternalLink(string number, string sessionId, string wuId)
            {
                return $"{Domain}/{VersionApi}/{DefaultLinkByGetInfoByUser}&pointer=%2B{number}&pointerSource=external&pointerType=phone&sessionid={sessionId}&wuid={wuId}";
            }


            public static string GetBankMemberLink(string sessionId, string wuId)
            {
                return $"{Domain}/{VersionApi}/sbp_dictionary?sessionid={sessionId}&wuid={wuId}";
            }

            public static string CreateNewOrderLink(string sessionId, string wuId)
            {
                return $"{Domain}/{VersionApi}/pay?appName=payments&sessionid={sessionId}&wuid={wuId}";
            }

            public static string GetBalanceLink(string sessionId)
            {
               return $"{Domain}/{VersionApi}/grouped_requests?_methods=accounts_flat&sessionid={sessionId}";
            }
        }
    }
}