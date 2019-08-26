using MessageBird.Objects;

namespace MessageBird.Resources.Conversations
{
    public class Contacts : ConversationsResource
    {
        public Contacts(Objects.Conversations.Contact contact, bool useWhatsappSandbox = false) : base("contacts", contact, useWhatsappSandbox) { }
        public Contacts(bool useWhatsappSandbox = false) : this(new Objects.Conversations.Contact(), useWhatsappSandbox) { }
        
    }
}