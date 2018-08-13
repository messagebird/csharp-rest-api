using System.Collections.Specialized;

namespace MessageBird.Resources
{
    public class ContactLists : BaseLists<Objects.Contact>
    {
        public ContactLists()
            : base("contacts", new Objects.ContactList())
        {
            //
        }
    }
}
