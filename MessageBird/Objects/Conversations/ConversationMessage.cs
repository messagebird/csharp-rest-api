using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageBird.Objects.Conversations
{
    public enum ConversationMessageDirection
    {
        [EnumMember(Value = "received")]
        Received,
        [EnumMember(Value = "sent")]
        Sent,
    }
    
    public enum ConversationMessageStatus
    {

        [EnumMember(Value = "deleted")]
        Deleted,
        [EnumMember(Value = "delivered")]
        Delivered,
        [EnumMember(Value = "failed")]
        Failed,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "read")]
        Read,
        [EnumMember(Value = "received")]
        Received,
        [EnumMember(Value = "sent")]
        Sent,
        [EnumMember(Value = "unsupported")]
        Unsupported,
        [EnumMember(Value = "rejected")]
        Rejected,
    }

    public class ConversationMessageError
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
    
    public class ConversationMessage : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("conversationId")]
        public string ConversationId { get; set; }  
        
        [JsonProperty("channelId")]
        public string ChannelId { get; set; }
        
        [JsonProperty("direction"), JsonConverter(typeof(StringEnumConverter))]
        public ConversationMessageDirection Direction {get; set;}
        
        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public ConversationMessageStatus Status {get; set;}
        
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public ContentType Type {get; set;}
        
        [JsonProperty("content")]
        public Content Content {get; set;}
        
        [JsonProperty("error")]
        public ConversationMessageError Error { get; set; }
        
        [JsonProperty("createdDatetime")]
        public DateTime? CreatedDatetime {get; set;}
        
        [JsonProperty("updatedDatetime")]
        public DateTime? UpdatedDatetime {get; set;}
    }
}