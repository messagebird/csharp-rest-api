using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MessageBird.Objects.Conversations
{
    public enum WebhookEvent
    {
        [EnumMember(Value = "conversation.created")]
        ConversationCreated,
        [EnumMember(Value = "conversation.updated")]
        ConversationUpdated,
        [EnumMember(Value = "message.created")]
        MessageCreated,
        [EnumMember(Value = "message.updated")]
        MessageUpdated,
    }
    
    public class Webhook : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id {get;set;}
        
        [JsonProperty("channelId")]
        public string ChannelId {get;set;}
        
        [JsonProperty("url")]
        public string Url {get;set;}
        
        [JsonProperty("events")]
        public List<WebhookEvent> Events {get;set;}
        
        [JsonProperty("createdDatetime")]
        public DateTime? CreatedDatetime {get;set;}
        
        [JsonProperty("updatedDatetime")]
        public DateTime? UpdatedDatetime {get;set;}
    }
}