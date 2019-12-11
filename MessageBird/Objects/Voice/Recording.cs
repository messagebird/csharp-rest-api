using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MessageBird.Objects.Voice
{
    public class Recording : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("legId")]
        public string LegId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("duration")]
        public int? Duration { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("_links")]
        public Dictionary<string, string> Links { get; set; }

        public string CallId { get; set; }
    }

    public class RecordingList : VoiceBaseList<Recording>
    {
        public string LegId { get; set; }

        public string CallId { get; set; }
    }
}
