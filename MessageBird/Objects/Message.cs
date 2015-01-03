using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using MessageBird.Json.Converters;


namespace MessageBird.Objects
{
    public enum Direction
    {
        [EnumMember(Value = "mt")]
        MobileTerminated,
        [EnumMember(Value = "mo")]
        MobileOriginated
    };
    public enum MessageType
    {
        [EnumMember(Value = "sms")]
        Sms,
        [EnumMember(Value = "binary")]
        Binary,
        [EnumMember(Value = "premium")]
        Premium,
        [EnumMember(Value = "flash")]
        Flash
    };
    public enum DataEncoding
    {
        [EnumMember(Value = "plain")]
        Plain,
        [EnumMember(Value = "unicode")]
        Unicode
    };
    public enum MessageClass { Flash = 0, Normal };

    public class MessageOptionalArguments
    {
        public MessageType Type { get; set; }
        public string Reference { get; set; }
        public int? Validity { get; set; }
        public int? Gateway { get; set; }
        public Hashtable TypeDetails { get; set; }
        public DataEncoding Encoding { get; set; }
        public MessageClass Class { get; set; }
        public DateTime? Scheduled { get; set; }

        public MessageOptionalArguments()
        {
            Type = MessageType.Sms;
            Encoding = DataEncoding.Plain;
            Class = MessageClass.Normal;
        }
    }

    public class Message : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("direction")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Direction? Direction { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageType Type { get; set; }

        private string originator;
        [JsonProperty("originator")]
        public string Originator
        {
            get
            {
                return originator;
            }
            set
            {
                var numeric = new Regex("^\\+?[0-9]+$");
                var alphanumericWithWhitespace = new Regex("^[A-Za-z0-9]+(?:\\s[A-Za-z0-9]+)*$");
                if (string.IsNullOrEmpty(value) || numeric.IsMatch(value))
                {
                    originator = value.TrimStart(new [] {'+'});
                }
                else if (alphanumericWithWhitespace.IsMatch(value))
                {
                    if (value.Length <= 11)
                    {
                        originator = value;
                    }
                    else
                    {
                        throw new ArgumentException("Alphanumeric originator is limited to 11 characters.");
                    }
                }
                else
                {
                    throw new ArgumentException("Originator can only contain numeric or whitespace separated alphanumeric characters.");
                }
            }
        }

        [JsonProperty("body")]
        public string Body { get; set; }

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
        public MessageClass Class { get; set; }

        [JsonProperty("scheduledDatetime"), JsonConverter(typeof(RFC3339DateTimeConverter))]
        public DateTime? Scheduled { get; set; }

        [JsonProperty("createdDatetime"), JsonConverter(typeof(RFC3339DateTimeConverter))]
        public DateTime? Created { get; set; }

        [JsonProperty("recipients"), JsonConverter(typeof(RecipientsConverter))]
        public Recipients Recipients { get; set; }

        public Message()
        {
        }

        public Message(string id)
        {
            Id = id;
        }

        public Message(string originator, string body, Recipients recipients, MessageOptionalArguments optionalArguments = null)
        {
            Originator = originator;
            Body = body;
            Recipients = recipients;

            optionalArguments = optionalArguments ?? new MessageOptionalArguments();

            Type = optionalArguments.Type;
            Reference = optionalArguments.Reference;
            Validity = optionalArguments.Validity;
            Gateway = optionalArguments.Gateway;
            TypeDetails = optionalArguments.TypeDetails;
            Encoding = optionalArguments.Encoding;
            Class = optionalArguments.Class;
            Scheduled = optionalArguments.Scheduled;
        }

        public override string ToString()
        {
            // When converting a message object to json via the ToString method,
            // we serialize the entire object and do not apply conversions required by the
            // MessageBird endpoint.
            Recipients.SerializeMsisdnsOnly = false;
            var settings = new JsonSerializerSettings
            {
                Formatting =  Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
            string serializedMessage = JsonConvert.SerializeObject(this, settings);
            Recipients.SerializeMsisdnsOnly = true;
            return serializedMessage;
        }
    }
}
