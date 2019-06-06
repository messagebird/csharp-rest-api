using System;

namespace MessageBird.Resources.Conversations
{
    public class MessageLists : BaseLists<Objects.Conversations.ConversationMessage>
    {
        public MessageLists()
            : base("messages", new Objects.Conversations.ConversationMessageList())
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

        public override string BaseUrl
        {
            get { return Conversations.ConverstationsBaseUrl; }
        }
    }
}