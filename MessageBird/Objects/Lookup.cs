using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System;

namespace MessageBird.Objects
{
    public enum PhoneNumberType
    {
        [EnumMember(Value = "fixed line")]
        FixedLine,
        [EnumMember(Value = "mobile")]
        Mobile,
        [EnumMember(Value = "fixed line or mobile")]
        FixedLineOrMobile,
        [EnumMember(Value = "toll free")]
        TollFree,
        [EnumMember(Value = "premium rate")]
        PremiumRate,
        [EnumMember(Value = "shared cost")]
        SharedCost,
        [EnumMember(Value = "voip")]
        Voip,
        [EnumMember(Value = "personal number")]
        PersonalNumber,
        [EnumMember(Value = "pager")]
        Pager,
        [EnumMember(Value = "universal access number")]
        UniversalAccessNumber,
        [EnumMember(Value = "voice mail")]
        VoiceMail,
        [EnumMember(Value = "unknown")]
        Unknown
    };

    public class LookupOptionalArguments
    {
        public string CountryCode { get; set; }
    }

    public class Lookup : IIdentifiable<string>
    {
        /// <summary>
        /// To uniformly treat objects, we implement the IIdentifiable interface even though
        /// a Lookup object doesn't have an id!
        /// By returning null, the Lookup object signals the user of a Lookup object
        /// (currently always the Lookup resource) to ignore the id property.
        /// </summary>
        /// <remarks>Throwing an exception will interfere with serialization of a Lookup object.</remarks>
        public string Id
        {
            get
            {
                return null;
            }
        }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; private set; }

        [JsonProperty("countryPrefix")]
        public int CountryPrefix { get; set; }

        [JsonProperty("phoneNumber")]
        public long PhoneNumber { get; set; }

        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public PhoneNumberType Type { get; set; }

        [JsonProperty("formats")]
        public Formats Formats { get; set; }

        [JsonProperty("hlr")]
        public Hlr Hlr { get; set; }

        public Lookup()
        {
        }

        public Lookup(long phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public Lookup(long phoneNumber, LookupOptionalArguments optionalArguments = null)
        {
            PhoneNumber = phoneNumber;

            optionalArguments = optionalArguments ?? new LookupOptionalArguments();

            CountryCode = optionalArguments.CountryCode;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
