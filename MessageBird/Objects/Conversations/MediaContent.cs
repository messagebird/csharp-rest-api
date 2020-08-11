using Newtonsoft.Json;

namespace MessageBird.Objects.Conversations
{
    public class MediaContent
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        
        [JsonProperty("caption")]
        public string Caption { get; set; }
    }
}
