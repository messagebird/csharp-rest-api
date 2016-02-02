using System;

namespace MessageBird.Resources
{
    class LookupHlr : Resource
    {
        public LookupHlr(Objects.LookupHlr lookupHlr)
            : base("lookup", lookupHlr)
        {
        }

        protected bool HasCountryCode
        {
            get
            {
                return (Object != null) && !String.IsNullOrEmpty(((Objects.LookupHlr)Object).CountryCode);
            }
        }

        public override string Uri
        {
            get
            {
                return String.Format("{0}/{1}/hlr", Name, ((Objects.LookupHlr)Object).Msisdn);
            }
        }

        public override string QueryString
        {
            get
            {
                return HasCountryCode ? "countryCode=" + System.Uri.EscapeDataString(((Objects.LookupHlr)Object).CountryCode) : String.Empty;
            }
        }
    }
}
