using System.Text.Json.Serialization;

namespace SellSpasibo.BLL.Models
{
    public class PaymentDetails
    {
        [JsonPropertyName("pointerType")]
        public string PointerType { get; set; }
        [JsonPropertyName("pointer")]
        public string Pointer { get; set; }
        [JsonPropertyName("pointerLinkId")]
        public string PointerLinkId { get; set; }
        [JsonPropertyName("maskedFIO")]
        public string MaskedFIO { get; set; }
        [JsonPropertyName("bankMemberId")]
        public string BankMemberId { get; set; }
    }
}
