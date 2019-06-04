using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageBird.Objects.Conversations
{
    public class ConversationStartRequest
    {
        [JsonProperty("to")]
        public string To { get; set; }
        
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public ContentType Type {get; set;}
    
        [JsonProperty("content")]
        public Content Content {get; set;}
        
        [JsonProperty("channelId")]
        public string ChannelId { get; set; }
    }
}