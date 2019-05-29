using Newtonsoft.Json;

namespace MessageBird.Objects.Conversations
{
    public class LocationContent
    {
        [JsonProperty("latitude")]
        public double Latitude {get;set;}
        
        [JsonProperty("longitude")]
        public double Longitude {get;set;}
    }
}