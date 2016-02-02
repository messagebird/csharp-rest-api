using Newtonsoft.Json;

namespace MessageBird.Objects
{
    public class Formats
    {
        [JsonProperty("e164")]
        public string E164 { get; set; }

        [JsonProperty("international")]
        public string International { get; set; }

        [JsonProperty("national")]
        public string National { get; set; }

        [JsonProperty("rfc3966")]
        public string Rfc3966 { get; set; }
    }
}
