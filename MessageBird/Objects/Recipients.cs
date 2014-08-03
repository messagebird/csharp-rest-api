using System.Collections.Generic;
using Newtonsoft.Json;

namespace MessageBird.Objects
{
    public class Recipients
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; private set; }

        [JsonProperty("totalSentCount")]
        public int TotalSentCount { get; private set; }

        [JsonProperty("totalDeliveredCount")]
        public int TotalDeliveredCount { get; private set; }

        [JsonProperty("totalDeliveryFailedCount")]
        public int TotalDeliveryFailedCount { get; private set; }

        [JsonProperty("items")]
        public List<Recipient> Items {get; set;}

        public Recipients()
        {
            Items = new List<Recipient>();
        }

        public Recipients(long[] msisdns) : this()
        {
            foreach (long msisdn in msisdns)
            {
                AddRecipient(msisdn);
            }
        }

        public void AddRecipient(long msisdn)
        {
            Items.Add(new Recipient(msisdn));
        }

    }
}
