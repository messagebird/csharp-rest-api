using Newtonsoft.Json;

namespace MessageBird.Objects.Conversations
{
    public class LocationContent
    {
        [JsonProperty("latitude")]
        public float Latitude {get;set;}
        
        [JsonProperty("longitude")]
        public float Longitude {get;set;}
    }
}