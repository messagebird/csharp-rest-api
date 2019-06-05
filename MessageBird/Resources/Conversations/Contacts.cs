using MessageBird.Objects;

namespace MessageBird.Resources.Conversations
{
    public class Contacts : Resource
    {
        public Contacts(Objects.Conversations.Contact contact) : base("contacts", contact) { }
        public Contacts() : this(new Objects.Conversations.Contact()) { }
        public override string BaseUrl
        {
            get { return Conversations.ConverstationsBaseUrl; }
        }
    }
}