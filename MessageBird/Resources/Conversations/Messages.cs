using System;

namespace MessageBird.Resources.Conversations
{
    public class Messages : ConversationsResource
    {
        public Messages(Objects.Conversations.ConversationMessage conversationMessage, bool useWhatsAppSandbox = false) : base("messages", conversationMessage, useWhatsAppSandbox) { }
        public Messages(bool useWhatsAppSandbox = false) : this(new Objects.Conversations.ConversationMessage(), useWhatsAppSandbox) { }

        private string BaseName
        {
            get
            {
                return String.Format("conversations/{0}/{1}",
                    ((Objects.Conversations.ConversationMessageList) Object).ConversationId,
                    Name);
            }
        }

        public override string Uri
        {
            get { return HasId ? string.Format("{0}/{1}", Name, Id) : BaseName; }
        }

        
    }
}