using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageBird.Objects.Conversations
{
    public class MessageSendRequest
    {
        [JsonIgnore]
        public string ConversationId { get; set; }
        
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public ContentType Type {get;set;}
        
        [JsonProperty("content")]
        public Content Content {get;set;}
        
        [JsonProperty("channelId")]
        public string ChannelId {get;set;}
        
        [JsonProperty("fallback")]
        public MessageFallBack FallBack { get; set; }
    }
}