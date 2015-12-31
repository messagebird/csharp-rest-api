using System;

namespace MessageBird.Resources
{
    class Lookup : Resource
    {
        public Lookup(Objects.Lookup lookup)
            : base("lookup", lookup)
        {
        }

        protected bool HasCountryCode
        {
            get
            {
                return (Object != null) && !String.IsNullOrEmpty(((Objects.Lookup)Object).CountryCode);
            }
        }

        public override string Uri
        {
            get
            {
                return String.Format("{0}/{1}", Name, ((Objects.Lookup)Object).PhoneNumber);
            }
        }

        public override string QueryString
        {
            get
            {
                return HasCountryCode ? "countryCode=" + System.Uri.EscapeDataString(((Objects.Lookup)Object).CountryCode) : String.Empty;
            }
        }
    }
}
