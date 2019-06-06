using Newtonsoft.Json;

namespace MessageBird.Objects.Conversations
{
    public class ConversationMessageFallBack
    {
        [JsonProperty("from")]
        public string From { get; set; }
        
        [JsonProperty("after")]
        public string After { get; set; }
    }
}