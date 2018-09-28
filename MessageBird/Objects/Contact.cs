using MessageBird.Json.Converters;
using Newtonsoft.Json;
using System;

namespace MessageBird.Objects
{
    public class Contact : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("msisdn")]
        public long Msisdn { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("customDetails")]
        public ContactCustomDetails CustomDetails { get; set; }

        [JsonProperty("groups")]
        public ContactGroupReference GroupReference { get; set; }

        [JsonProperty("messages")]
        public ContactMessageReference MessageReference { get; set; }

        [JsonProperty("createdDatetime"), JsonConverter(typeof(RFC3339DateTimeConverter))]
        public DateTime? CreatedDatetime;

        [JsonProperty("updatedDatetime"), JsonConverter(typeof(RFC3339DateTimeConverter))]
        public DateTime? UpdatedDatetime;

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
            RequestObject requestObject = ToRequestObject();

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
            [JsonProperty("msisdn")]
            public long? Msisdn { get; set; }

            [JsonProperty("firstName")]
            public string FirstName { get; set; }

            [JsonProperty("lastName")]
            public string LastName { get; set; }

            [JsonProperty("custom1")]
            public string Custom1 { get; set; }

            [JsonProperty("custom2")]
            public string Custom2 { get; set; }

            [JsonProperty("custom3")]
            public string Custom3 { get; set; }

            [JsonProperty("custom4")]
            public string Custom4 { get; set; }

            public RequestObject(Contact contact)
            {
                FirstName = contact.FirstName;
                LastName = contact.LastName;
                
                if (contact.Msisdn != default(long))
                {
                    Msisdn = contact.Msisdn;
                }

                if (contact.CustomDetails != null)
                {
                    Custom1 = contact.CustomDetails.Custom1;
                    Custom2 = contact.CustomDetails.Custom2;
                    Custom3 = contact.CustomDetails.Custom3;
                    Custom4 = contact.CustomDetails.Custom4;
                }
            }
        }
    }
}
