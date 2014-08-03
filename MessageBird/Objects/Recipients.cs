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
        public Recipient[] Items {get; set;}

        public Recipients(Recipient[] items)
        {
            Items = items;
            TotalCount = items.Length;
        }

    }
}
