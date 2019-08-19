using MessageBird.Objects;

namespace MessageBird.Resources.Conversations
{
    public class Contacts : ConversationsResource
    {
        public Contacts(Objects.Conversations.Contact contact, bool useWhatsappSandbox) : base("contacts", contact, useWhatsappSandbox) { }
        public Contacts(bool useWhatsappSandbox) : this(new Objects.Conversations.Contact(), useWhatsappSandbox) { }
        
    }
}