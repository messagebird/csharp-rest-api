using Newtonsoft.Json;
using System.Collections.Generic;

namespace MessageBird.Objects.Voice
{

    public class VoiceResponse<T> : IIdentifiable<string>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }

        [JsonProperty("_links")]
        public Dictionary<string, string> Links { get; set; }

        public string Id
        {
            get
            {
                return string.Empty;
            }
        }
    }
}