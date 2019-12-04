using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageBird.Objects.Voice
{

    public class Webhook : IIdentifiable<string>
    {

        [JsonProperty("id")] 
        public string Id { get; set; }

        [JsonProperty("url")] 
        public string url { get; set; }

        [JsonProperty("token")] 
        public string token { get; set; }      

        [JsonProperty("createdAt")] 
        public DateTime? createdAt { get; set; }

        [JsonProperty("updatedAt")] 
        public DateTime? updatedAt { get; set; }
    }
}