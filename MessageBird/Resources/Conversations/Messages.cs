using System;

namespace MessageBird.Resources.Conversations
{
    public class Messages : Resource
    {
        public Messages(Objects.Conversations.ConversationMessage conversationMessage) : base("messages", conversationMessage) { }
        public Messages() : this(new Objects.Conversations.ConversationMessage()) { }

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

        public override string BaseUrl
        {
            get { return ConverstationsBaseUrl; }
        }
    }
}