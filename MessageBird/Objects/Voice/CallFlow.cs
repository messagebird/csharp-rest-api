using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MessageBird.Objects.Voice
{
    public class CallFlow : IIdentifiable<string>
    {
        public CallFlow()
        {
            Steps = new List<Step>();
            Record = null;
        }

        [JsonProperty("id")] 
        public string Id { get; set; }

        [JsonProperty("title")] 
        public string Title { get; set; }

        [JsonProperty("record")] 
        public bool? Record { get; set; }

        [JsonProperty("steps")] 
        public List<Step> Steps { get; set; }

        [JsonProperty("createdAt")] 
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updatedAt")] 
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("_links")]
        public Dictionary<string, string> Links { get; set; }

        /// <summary>
        /// Requests to the Contacts API use a different format than responses.
        /// ToRequestObject gets a request object in the proper format.
        /// </summary>
        /// <returns>
        /// A request object that can be used for serializing.
        /// </returns>
        public RequestObject ToRequestObject()
        {
            return new RequestObject(this);
        }

        public override string ToString()
        {
            var requestObject = ToRequestObject();

            return JsonConvert.SerializeObject(requestObject, Formatting.Indented);
        }

        /// <summary>
        /// Object that can be used for serializing to JSON when making
        /// requests. That requires having the custom details on the top-level
        /// object rather than nested, and Json.Net does not offer "flattened"
        /// objects out of the box.
        /// </summary>
        public class RequestObject
        {
            public RequestObject()
            {
                Steps=new List<Step>();
            }

            [JsonProperty("id")] 
            public string Id { get; set; }

            [JsonProperty("title")] 
            public string Title { get; set; }

            [JsonProperty("record")] 
            public bool? Record { get; set; }

            [JsonProperty("steps")] 
            public List<Step> Steps { get; set; }

            [JsonProperty("_links")]
            public Dictionary<string, string> Links { get; set; }

            public RequestObject(CallFlow callFlow)
            {
                Title = callFlow.Title;
                Record = callFlow.Record;
                Steps = callFlow.Steps;
            }
        }
    }

    public class CallFlowList : VoiceBaseList<CallFlow>
    {
    }   
    
    public class Options
    {
        [JsonProperty("destination")] 
        public string Destination { get; set; }
    }

    public class Step
    {
        [JsonProperty("id")] 
        public string Id { get; set; }

        [JsonProperty("action")] 
        public string Action { get; set; }

        [JsonProperty("options")] 
        public Options Options { get; set; }
    }
}