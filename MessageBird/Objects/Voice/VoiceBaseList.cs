using Newtonsoft.Json;

namespace MessageBird.Objects.Voice
{

    public class Pagination
    {
        [JsonProperty("totalCount")] 
        public int TotalCount { get; set; }

        [JsonProperty("pageCount")] 
        public int PageCount { get; set; }

        [JsonProperty("currentPage")] 
        public int CurrentPage { get; set; }

        [JsonProperty("perPage")] 
        public int PerPage { get; set; }
    };

    public class VoiceBaseList<T> : VoiceResponse<T>
    {
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

    }
}