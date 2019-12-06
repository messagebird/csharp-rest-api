using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MessageBird.Json.Converters;

namespace MessageBird.Objects
{
    public enum IfMachineOptions
    {
        [EnumMember(Value = "continue")]
        Continue,
        [EnumMember(Value = "delay")]
        Delay,
        [EnumMember(Value = "hangup")]
        Hangup
    }

    public class VoiceMessageOptionalArguments
    {
        public string Reference { get; set; }
        public string ReportUrl { get; set; }
        public string Originator { get; set; }
        public Common.Language Language { get; set; }
        public Common.Voice Voice { get; set; }
        public int Repeat { get; set; }
        public IfMachineOptions IfMachine { get; set; }
        public DateTime? Scheduled { get; set; }

        public VoiceMessageOptionalArguments()
        {
            Language = Common.Language.English;
            Voice = Common.Voice.Female;
            Repeat = 1;
            IfMachine = IfMachineOptions.Continue;
        }
    }

    public class VoiceMessage : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("reportUrl")]
        public string ReportUrl { get; set; }

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
                if (!String.IsNullOrEmpty(value) && !numeric.IsMatch(value))
                {
                    throw new ArgumentException("Originator can only contain numeric characters.");
                }

                // Only trim the '+' sign value if it's not empty or null
                if (!String.IsNullOrEmpty(value))
                {
                    originator = value.TrimStart(new[] { '+' });
                }
                else
                {
                    originator = value;
                }
            }
        }

        private string body;
        [JsonProperty("body")]
        public string Body
        {
            get { return body; }
            set
            {
                // XXX: Create const to hold the max length
                if (!String.IsNullOrEmpty(value) && value.Length > 1000)
                {
                    throw new ArgumentException("The maximum body length is 1000 characters");
                }
                body = value;
            }
        }

        [JsonProperty("language"), JsonConverter(typeof(StringEnumConverter))]
        public Common.Language Language { get; set; }

        [JsonProperty("voice"), JsonConverter(typeof(StringEnumConverter))]
        public Common.Voice Voice { get; set; }

        [JsonProperty("repeat")]
        public int Repeat { get; set; }

        [JsonProperty("ifMachine"), JsonConverter(typeof(StringEnumConverter))]
        public IfMachineOptions IfMachine { get; set; }

        [JsonProperty("scheduledDatetime"), JsonConverter(typeof(RFC3339DateTimeConverter))]
        public DateTime? Scheduled { get; set; }

        [JsonProperty("createdDatetime"), JsonConverter(typeof(RFC3339DateTimeConverter))]
        public DateTime? Created { get; set; }

        [JsonProperty("recipients"), JsonConverter(typeof(RecipientsConverter))]
        public Recipients Recipients { get; set; }

        public VoiceMessage()
        {
        }

        public VoiceMessage(string id)
        {
            Id = id;
        }

        public VoiceMessage(string body, Recipients recipients, VoiceMessageOptionalArguments optionalArguments = null)
        {
            Body = body;
            Recipients = recipients;

            optionalArguments = optionalArguments ?? new VoiceMessageOptionalArguments();

            Reference = optionalArguments.Reference;
            ReportUrl = optionalArguments.ReportUrl;
            Originator = optionalArguments.Originator;
            Language = optionalArguments.Language;
            Voice = optionalArguments.Voice;
            Repeat = optionalArguments.Repeat;
            IfMachine = optionalArguments.IfMachine;
            Scheduled = optionalArguments.Scheduled;
        }

        public override string ToString()
        {
            // When converting a voicemessage object to json via the ToString method,
            // we serialize the entire object and do not apply conversions required by the
            // MessageBird endpoint.
            Recipients.SerializeMsisdnsOnly = false;
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
            string serializedMessage = JsonConvert.SerializeObject(this, settings);
            Recipients.SerializeMsisdnsOnly = true;
            return serializedMessage;
        }
    }
}
