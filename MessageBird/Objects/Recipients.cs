namespace MessageBird.Objects
{
    public class Recipients
    {
        public int TotalCount { get; private set; }

        public int TotalSentCount { get; private set; }
        public int TotalDeliveredCount { get; private set; }
        public int TotalDeliveryFailedCount { get; private set; }
        public Recipient[] Items {get; set;}
        public Recipients(Recipient[] items)
        {
            Items = items;
            TotalCount = items.Length;
        }

    }
}
