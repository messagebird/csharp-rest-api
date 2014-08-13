using Newtonsoft.Json;

namespace MessageBird.Objects
{
    public enum ErrorCode
    {
        RequestNotAllowed = 2,
        MissingParameters = 9,
        InvalidParameters = 10,
        NotFound = 20,
        NotEnoughBalance = 25,
        ApiNotFound = 98,
        InternalError = 99
    }

    public class Error
    {
        [JsonProperty("code")]
        public ErrorCode Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("parameter")]
        public string Parameter { get; set; }
    }
}
