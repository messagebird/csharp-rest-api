using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageBird.Objects.Conversations
{
    public enum ChannelStatus
    {
        [EnumMember(Value = "activating")]
        Activating,
        [EnumMember(Value = "activation_code_required")]
        ActivationCodeRequired,
        [EnumMember(Value = "activation_required")]
        ActivationRequired,
        [EnumMember(Value = "active")]
        Active,
        [EnumMember(Value = "deleted")]
        Deleted,
        [EnumMember(Value = "inactive")]
        Inactive,
        [EnumMember(Value = "pending")]
        Pending,
    }
    public class Channel : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id {get; set;}
        
        [JsonProperty("name")]
        public string Name {get; set;}
        
        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public ChannelStatus Status {get; set;}
        
        [JsonProperty("createdDatetime")]
        public DateTime? CreatedDatetime {get; set;}
        
        [JsonProperty("updatedDatetime")]
        public DateTime? UpdatedDatetime {get; set;}
    }
}