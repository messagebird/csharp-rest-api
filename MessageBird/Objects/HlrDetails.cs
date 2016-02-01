using Newtonsoft.Json;

namespace MessageBird.Objects
{
    public class HlrDetails
    {
        [JsonProperty("status_desc")]
        public string StatusDesc { get; set; }

        [JsonProperty("imsi")]
        public string Imsi { get; set; }

        [JsonProperty("country_iso")]
        public string CountryIso { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("location_msc")]
        public string LocationMsc { get; set; }

        [JsonProperty("location_iso")]
        public string LocationIso { get; set; }

        [JsonProperty("ported")]
        public int Ported { get; set; }

        [JsonProperty("roaming")]
        public int Roaming { get; set; }
    }
}
