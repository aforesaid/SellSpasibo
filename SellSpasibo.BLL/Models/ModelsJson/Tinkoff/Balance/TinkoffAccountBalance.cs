﻿using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models.ModelsJson.Tinkoff.Balance
{
    public class TinkoffAccountBalance
    {
        [JsonPropertyName("currency")]
        public TinkoffCurrency Currency { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }
    }
}
