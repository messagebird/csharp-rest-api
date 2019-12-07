using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        public CallStatus? status { get; set; }

        [JsonProperty("source")] 
        public string source { get; set; }

        [JsonProperty("destination")] 
        public string destination { get; set; }      
         
        [JsonProperty("webhook")] 
        public Webhook webhook { get; set; }

        [JsonProperty("callFlow")] 
        public CallFlow callFlow { get; set; }

        [JsonProperty("duration")] 
        public int duration { get; set; }

        [JsonProperty("createdAt")] 
        public DateTime? createdAt { get; set; }

        [JsonProperty("updatedAt")] 
        public DateTime? updatedAt { get; set; }

        [JsonProperty("endedAt")] 
        public DateTime? endedAt { get; set; }
    }

    public class CallResponse : IIdentifiable<string>
    {
        [JsonProperty("data")]
        public List<Call> Data { get; set; }

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

    public class CallList : CallResponse
    {
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}