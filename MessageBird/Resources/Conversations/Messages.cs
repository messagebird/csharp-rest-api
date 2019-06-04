using System;

namespace MessageBird.Resources.Conversations
{
    public class Messages : Resource
    {
        public Messages(Objects.Conversations.Message message) : base("messages", message) { }
        public Messages() : this(new Objects.Conversations.Message()) { }

        private string BaseName =>
            String.Format("conversations/{0}/{1}", ((Objects.Conversations.MessageList) Object).ConversationId, Name);
        
        public override string Uri => HasId ? String.Format("{0}/{1}", Name, Id) : BaseName;
        public override string Endpoint => Conversations.ConverstationsEndpoint;
    }
}