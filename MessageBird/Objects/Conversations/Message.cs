using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MessageBird.Objects.Conversations
{
    public enum MessageDirection
    {
        [EnumMember(Value = "received")]
        Received,
        [EnumMember(Value = "received")]
        Sent,
    }
    
    public enum MessageStatus
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
    }
    
    public class Message : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("conversationId")]
        public string ConversationId { get; set; }  
        
        [JsonProperty("channelId")]
        public string ChannelId { get; set; }
        
        [JsonProperty("direction")]
        private MessageDirection Direction {get; set;}
        
        [JsonProperty("status")]
        private MessageStatus Status {get; set;}
        
        [JsonProperty("type")]
        private ContentType Type {get; set;}
        
        [JsonProperty("content")]
        private Content Content {get; set;}
        
        [JsonProperty("createdDatetime")]
        private DateTime? CreatedDatetime {get; set;}
        
        [JsonProperty("updatedDatetime")]
        private DateTime? UpdatedDatetime {get; set;}
    }
}