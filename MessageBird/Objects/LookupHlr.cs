using Newtonsoft.Json;

namespace MessageBird.Objects
{
    public class LookupHlrOptionalArguments
    {
        public string CountryCode { get; set; }
    }

    public class LookupHlr : Hlr
    {
        [JsonProperty("countryCode")]
        public string CountryCode { get; private set; }

        public LookupHlr()
        {
        }

        public LookupHlr(long phonenumber, LookupHlrOptionalArguments optionalArguments = null)
        {
            Msisdn = phonenumber;

            optionalArguments = optionalArguments ?? new LookupHlrOptionalArguments();

            CountryCode = optionalArguments.CountryCode;
        }

        public LookupHlr(long phonenumber, string reference, LookupHlrOptionalArguments optionalArguments = null)
            : base(phonenumber, reference)
        {
            optionalArguments = optionalArguments ?? new LookupHlrOptionalArguments();

            CountryCode = optionalArguments.CountryCode;
        }
    }
}
