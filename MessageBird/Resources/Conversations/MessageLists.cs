using System;

namespace MessageBird.Resources.Conversations
{
    public class MessageLists : BaseLists<Objects.Conversations.Message>
    {
        public MessageLists()
            : base("messages", new Objects.Conversations.MessageList())
        { }

        public override string Uri => String.Format("conversations/{0}/{1}", ((Objects.Conversations.MessageList)Object).ConversationId, Name);
        
        public override string Endpoint => Conversations.ConverstationsEndpoint;
    }
}