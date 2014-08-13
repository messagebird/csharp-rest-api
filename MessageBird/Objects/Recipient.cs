using System;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


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
            // Voice message status
            [EnumMember(Value = "calling")]
            Calling,
            [EnumMember(Value = "answered")]
            Answered,
            [EnumMember(Value = "failed")]
            Failed,
            // reserved for future use
            [EnumMember(Value = "busy")]
            Busy,
            [EnumMember(Value = "machine")]
            Machine
        };

        [JsonProperty("recipient")]
        public long Msisdn { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RecipientStatus? Status { get; set; }

        [JsonProperty("statusDatetime")]
        public DateTime? StatusDatetime { get; set; }

        public Recipient(long msisdn)
        {
            Msisdn = msisdn;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
