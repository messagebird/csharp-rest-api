using Newtonsoft.Json;

namespace MessageBird.Objects
{
    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("description")]
        public string Description {get; set;}

        [JsonProperty("parameter")]
        public string Parameter {get; set;}

        public Error()
        {
        }
    }
}
