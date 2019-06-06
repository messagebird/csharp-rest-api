using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MessageBird.Objects.Conversations
{
    public class Contact : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id {get; set;}
        
        [JsonProperty("href")]
        public string Href {get; set;}
        
        [JsonProperty("msisdn")]
        public string Msisdn {get; set;}
        
        [JsonProperty("firstName")]
        public string FirstName {get; set;}
        
        [JsonProperty("lastName")]
        public string LastName {get; set;}
        
        [JsonProperty("customDetails")]
        public Dictionary<string, object> CustomDetails {get; set;}
        
        [JsonProperty("createdDatetime")]
        public DateTime? CreatedDatetime {get; set;}
        
        [JsonProperty("updatedDatetime")]
        public DateTime? UpdatedDatetime {get; set;}
    }
}