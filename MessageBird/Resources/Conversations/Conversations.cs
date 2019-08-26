using MessageBird.Net;

namespace MessageBird.Resources.Conversations
{
    public class Conversations : ConversationsResource
    {
        public Conversations(Objects.Conversations.Conversation conversation, bool useWhatsappSandbox = false) : base("conversations", conversation, useWhatsappSandbox) { }
        public Conversations(bool useWhatsappSandbox = false) : this(new Objects.Conversations.Conversation(),useWhatsappSandbox) { }

        public override UpdateMode UpdateMode
        {
            get { return UpdateMode.Patch; }
        }
    }
}