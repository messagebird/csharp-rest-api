using Newtonsoft.Json;

namespace MessageBird.Objects
{
    /// <summary>
    /// Links for paginating through a list response.
    /// </summary>
    public class Links
    {
        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
        
        [JsonProperty("last")]
        public string Last { get; set; }
    }
}
