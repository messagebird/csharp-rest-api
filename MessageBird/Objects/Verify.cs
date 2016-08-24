using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using MessageBird.Json.Converters;
using System.ComponentModel;

namespace MessageBird.Objects
{
    public enum VerifyStatus
    {
        [EnumMember(Value = "sent")]
        Sent,
        [EnumMember(Value = "verified")]
        Verified,
        [EnumMember(Value = "active")]
        Active,
        [EnumMember(Value = "expired")]
        Expired,
        [EnumMember(Value = "failed")]
        Failed,
    }

    public class VerifyOptionalArguments
    {
        public string Template { get; set; }
        public DataEncoding Encoding { get; set; }
        public string Originator { get; set; }
        public string Reference { get; set; }
        public MessageType Type { get; set; }
        public int Timeout { get; set; }
        public int TokenLength { get; set; }
        public Voice Voice { get; set; }
        public Language Language { get; set; }

        public VerifyOptionalArguments()
        {
            Type = MessageType.Sms;
            Encoding = DataEncoding.Plain;
            Timeout = 30;
            TokenLength = 6;
            Language = Language.English;
            Voice = Voice.Female;
        }
    }

    public class Verify : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("recipient")]
        public long Recipient { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("messages"), JsonConverter(typeof(MessageConverter))]
        public Message Message { get; set; }

        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public VerifyStatus? Status { get; set; }

        [JsonProperty("createdDatetime")]
        public DateTime? Created { get; set; }

        [JsonProperty("validUntilDatetime")]
        public DateTime? ValidUntil { get; set; }

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
                Utilities.ParameterValidator.IsValidOriginator(value);

                var numeric = new Regex("^\\+?[0-9]+$");
                if (!string.IsNullOrEmpty(value) && numeric.IsMatch(value))
                {
                    value = value.TrimStart(new[] { '+' });
                }

                originator = value;
            }
        }

        [JsonProperty("template")]
        public string Template { get; set; }

        [JsonProperty("datacoding"), DefaultValue(DataEncoding.Plain), JsonConverter(typeof(StringEnumConverter))]
        public DataEncoding Encoding { get; set; }

        [JsonProperty("tokenLenght"), DefaultValue(6)]
        public int TokenLength { get; set; }

        [JsonProperty("type"), DefaultValue(MessageType.Sms), JsonConverter(typeof(StringEnumConverter))]
        public MessageType Type { get; set; }

        [JsonProperty("timeout"), DefaultValue(30)]
        public int Timeout { get; set; }

        [JsonProperty("voice"), JsonConverter(typeof(StringEnumConverter))]
        public Voice Voice { get; set; }

        [JsonProperty("language"), JsonConverter(typeof(StringEnumConverter))]
        public Language Language { get; set; }

        [JsonProperty("Token")]
        public string Token { get; private set; }

        public Verify()
        {
            TokenLength = 6;
            Timeout = 30;
            Type = MessageType.Sms;
            Encoding = DataEncoding.Plain;
            Language = Language.English;
            Voice = Voice.Female;
        }

        public Verify(string id)
        {
            Id = id;
        }

        public Verify(string id, string token)
        {
            Id = id;
            Token = token;
        }

        // Alias for the old constructor so that it remains backwards compatible
        public Verify(string recipient, VerifyOptionalArguments arguments = null) : this(Convert.ToInt64(recipient), arguments)
        {
        }

        public Verify(long recipient, VerifyOptionalArguments arguments = null)
        {
            Recipient = recipient;

            arguments = arguments ?? new VerifyOptionalArguments();

            Template = arguments.Template;
            Encoding = arguments.Encoding;
            Originator = arguments.Originator;
            Reference = arguments.Reference;
            Type = arguments.Type;
            Timeout = arguments.Timeout;
            TokenLength = arguments.TokenLength;
            Voice = arguments.Voice;
            Language = arguments.Language;
        }

        public override string ToString()
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
            };

            return JsonConvert.SerializeObject(this, settings);
        }
    }
}
