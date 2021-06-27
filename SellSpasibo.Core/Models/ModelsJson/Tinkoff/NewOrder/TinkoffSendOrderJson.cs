﻿using System.Text.Json.Serialization;

namespace SellSpasibo.Core.Models.ModelsJson.Tinkoff.NewOrder
{
    public class TinkoffSendOrderJson
    {
        [JsonPropertyName("trackingId")]
        public string PaymentId { get; set; }
        [JsonPropertyName("payload")]
        public TinkoffPayloadByNewOrder Payload { get; set; }
    }
}