using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBird.Objects
{
    public class Recipient
    {
        public enum RecipientStatus { Scheduled, Sent, Buffered, Delivered, DeliveryFailed };

        public long Msisdn {get; set;}
        public RecipientStatus? Status {get; set;}
        public DateTime? StatusDatetime {get; set;}

        public Recipient(long msisdn)
        {
            Msisdn = msisdn;
        }
    }
}
