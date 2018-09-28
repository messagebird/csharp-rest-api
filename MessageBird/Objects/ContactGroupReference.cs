using Newtonsoft.Json;

namespace MessageBird.Objects
{
    public class ContactGroupReference
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}
