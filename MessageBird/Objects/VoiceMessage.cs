using System;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using MessageBird.Json.Converters;

namespace MessageBird.Objects
{

    public enum Language
    {
        [EnumMember(Value = "nl-nl")]
        Dutch,
        [EnumMember(Value = "de-de")]
        German,
        [EnumMember(Value = "en-gb")]
        English,
        [EnumMember(Value = "fr-fr")]
        French
    };

    public enum Voice
    {
        [EnumMember(Value = "male")]
        Male,
        [EnumMember(Value = "female")]
        Female,
    };

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
        public Language Language { get; set; }
        public Voice Voice { get; set; }
        public int Repeat { get; set; }
        public IfMachineOptions IfMachine { get; set; }
        public DateTime? Scheduled { get; set; }

        public VoiceMessageOptionalArguments()
        {
            Language = Language.English;
            Voice = Voice.Female;
            Repeat = 1;
            IfMachine = IfMachineOptions.Continue;
        }
    }

    public class VoiceMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

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
        public Language Language { get; set; }

        [JsonProperty("voice"), JsonConverter(typeof(StringEnumConverter))]
        public Voice Voice { get; set; }

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
            Language = optionalArguments.Language;
            Voice = optionalArguments.Voice;
            Repeat = optionalArguments.Repeat;
            IfMachine = optionalArguments.IfMachine;
            Scheduled = optionalArguments.Scheduled;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
