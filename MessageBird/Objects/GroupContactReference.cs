using Newtonsoft.Json;

namespace MessageBird.Objects
{
    public class GroupContactReference
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}
