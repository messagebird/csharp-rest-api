using MessageBird.Json.Converters;
using Newtonsoft.Json;
using System;

namespace MessageBird.Objects
{
    public class Group : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("contacts")]
        public GroupContactReference Contacts { get; set; }

        [JsonProperty("createdDatetime"), JsonConverter(typeof(RFC3339DateTimeConverter))]
        public DateTime? CreatedDatetime;

        [JsonProperty("updatedDatetime"), JsonConverter(typeof(RFC3339DateTimeConverter))]
        public DateTime? UpdatedDatetime;

        /// <summary>
        /// Don't serialize the ID, e.g. when updating.
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
            RequestObject requestObject = ToRequestObject();

            return JsonConvert.SerializeObject(requestObject, Formatting.Indented);
        }

        /// <summary>
        /// Object that can be used for serializing to JSON when making
        /// requests.
        /// </summary>
        public class RequestObject
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            public RequestObject(Group group)
            {
                Name = group.Name;
            }
        }
    }
}
