using System;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageBird.Objects
{
    public enum HlrStatus
    {
        [EnumMember(Value = "sent")]
        Sent,
        [EnumMember(Value = "absent")]
        Absent,
        [EnumMember(Value = "active")]
        Active,
        [EnumMember(Value = "unknown")]
        Unknown,
        [EnumMember(Value = "failed")]
        Failed,
    }

    public class Hlr : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("msisdn")]
        public long Msisdn { get; set; }

        [JsonProperty("network")]
        public int? Network { get; set; }

        [JsonProperty("details")]
        public HlrDetails Details { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public HlrStatus? Status { get; set; }

        [JsonProperty("createdDatetime")]
        public DateTime? Created { get; set; }

        [JsonProperty("statusDatetime")]
        public DateTime? LastStatus { get; set; }

        public Hlr()
        {
        }

        public Hlr(string id)
        {
            Id = id;
        }

        public Hlr(long msisdn, string reference)
        {
            Msisdn = msisdn;
            Reference = reference;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
