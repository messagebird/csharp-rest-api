using MessageBird.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace MessageBird.Objects
{
    // XXX: Find a better location for these enums.
    public enum Direction { MobileTerminated, MobileOriginated };
    public enum MessageType { Sms, Binary, Premium, Flash };
    public enum DataEncoding { Plain, Unicode };
    public enum MessageClass { Flash = 0, Normal };

    public class Message
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("href")]
        public string Href {get; set;}

        [JsonProperty("direction")]
        [JsonConverter(typeof(DirectionEnumConverter))]
        public Direction? Direction { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageType Type { get; set; }

        private string originator;
        [JsonProperty("originator")]
        public string Originator {
            get
            {
                return originator;
            }
            set
            {
                Regex numeric = new Regex("^[0-9]+$");
                Regex alphanumeric = new Regex("^[A-Za-z0-9]+$");
                if (string.IsNullOrEmpty(value) || numeric.IsMatch(value))
                {
                    originator = value;
                }
                else if (alphanumeric.IsMatch(value))
                {
                    if (value.Length <= 11)
                    {
                        originator = value;
                    }
                    else
                    {
                        throw new ArgumentException("Alphanumeric originator is limted to 11 characters.");
                    }
                }
                else
                {
                    throw new ArgumentException("Originator can only contain alphanumeric characters.");
                }
            }
        }

        [JsonProperty("body")]
        public string Body {get; set;}

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("validity")]
        public int? Validity { get; set; }

        [JsonProperty("gateway")]
        public int? Gateway;

        [JsonProperty("typeDetails")]
        public Hashtable TypeDetails { get; set; }

        [JsonProperty("datacoding")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DataEncoding Encoding { get; set; }

        [JsonProperty("mclass")]
        public MessageClass Class {get; set;}

        // XXX: Add  RFC3339 format (Y-m-d\TH:i:sP) serialization.
        [JsonProperty("scheduledDatetime")]
        public DateTime? Scheduled { get; set; }

        // XXX: Add  RFC3339 format (Y-m-d\TH:i:sP) serialization.
        [JsonProperty("createdDatetime")]
        public DateTime? Created { get; set; }

        [JsonProperty("recipients")]
        public Recipients Recipients {get; set;}

        public Message()
        {
        }

        public Message(string id)
        {
            Id = id;
        }

        public Message(string originator, string body, Recipients recipients)
        {
            Originator = originator;
            Body = body;
            Recipients = recipients;

            Type = MessageType.Sms;
            Encoding = DataEncoding.Plain;
            Class = MessageClass.Normal;
        }
    }
}
