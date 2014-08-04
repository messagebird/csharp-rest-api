using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MessageBird.Objects
{
    public class Recipient
    {
        public enum RecipientStatus 
        {
            // Message status
            [EnumMember(Value = "scheduled")]
            Scheduled,
            [EnumMember(Value = "sent")]
            Sent,
            [EnumMember(Value = "buffered")]
            Buffered,
            [EnumMember(Value = "delivered")]
            Delivered,
            [EnumMember(Value = "delivery_failed")]
            DeliveryFailed,
        };

        [JsonProperty("recipient")]
        public long Msisdn {get; set;}

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RecipientStatus? Status {get; set;}

        [JsonProperty("statusDatetime")]
        public DateTime? StatusDatetime {get; set;}

        public Recipient(long msisdn)
        {
            Msisdn = msisdn;
        }
    }
}
