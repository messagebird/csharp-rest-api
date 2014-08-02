namespace MessageBird.Objects
{
    public class Recipients
    {
        public int TotalCount;
        public int TotalSentCount;
        public int TotalDeliveredCount;
        public int TotalDeliveryFailedCount;
        public Recipient[] Items;

        public Recipients(Recipient[] items)
        {
            Items = items;
            TotalCount = items.Length;
        }

    }
}
