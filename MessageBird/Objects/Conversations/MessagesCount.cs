using Newtonsoft.Json;

namespace MessageBird.Objects.Conversations
{
    public class MessagesCount
    {
        [JsonProperty("href")]
        public string Href {get;set;}
        
        [JsonProperty("totalCount")]
        public int TotalCount {get;set;}
    }
}