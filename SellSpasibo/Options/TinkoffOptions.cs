using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellSpasibo.Options
{
    public class TinkoffOptions
    {
        public const string Tinkoff = "Tinkoff";
        public string SessionId { get; set; }
        public string WuId { get; set; }
        public string Account { get; set; }
    }
}
