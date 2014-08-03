using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageBird.Objects
{
    public class Recipient
    {
        public enum RecipientStatus { Scheduled, Sent, Buffered, Delivered, DeliveryFailed };

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
