using System;

namespace MessageBird.Resources.Conversations
{
    public class MessageLists : ConversationsBaseLists<Objects.Conversations.ConversationMessage>
    {
        public MessageLists(bool useWhatsAppSandbox = false)
            : base("messages", new Objects.Conversations.ConversationMessageList(), useWhatsAppSandbox)
        { }

        public override string Uri
        {
            get
            {
                return String.Format("conversations/{0}/{1}",
                    ((Objects.Conversations.ConversationMessageList) Object).ConversationId,
                    Name);
            }
        }
    }
}