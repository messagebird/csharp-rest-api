using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MessageBird.Objects.Voice
{

    public enum CallStatus
    {
        [EnumMember(Value = "queued")]
        Queued,
        [EnumMember(Value = "starting")]
        Starting,
        [EnumMember(Value = "ongoing")]
        Ongoing,
        [EnumMember(Value = "ended")]
        Ended 
    };

    public class Call : IIdentifiable<string>
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CallStatus? Status { get; set; }

        [JsonProperty("source")] 
        public string Source { get; set; }

        [JsonProperty("destination")] 
        public string Destination { get; set; }      
         
        [JsonProperty("webhook")] 
        public Webhook Webhook { get; set; }

        [JsonProperty("callFlow")] 
        public CallFlow CallFlow { get; set; }

        [JsonProperty("duration")] 
        public int Duration { get; set; }

        [JsonProperty("createdAt")] 
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updatedAt")] 
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("endedAt")] 
        public DateTime? EndedAt { get; set; }
        
        [JsonProperty("_links")]
        public Dictionary<string, string> Links { get; set; }
    }

    public class CallList : VoiceBaseList<Call>
    {
    }
}