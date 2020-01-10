using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MessageBird.Objects.Voice
{
    public class Transcription : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("recordingId")]
        public string RecordingId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("_links")]
        public Dictionary<string, string> Links { get; set; }

        public string CallId { get; set; }

        public string LegId { get; set; }

        public string Language { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class TranscriptionList : VoiceBaseList<Transcription>
    {
        public string CallId { get; set; }

        public string LegId { get; set; }

        public string RecordingId { get; set; }
    }
}
