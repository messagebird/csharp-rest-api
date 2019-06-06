namespace MessageBird.Resources.Conversations
{
    public class ConversationLists : BaseLists<Objects.Conversations.Conversation>
    {
        public ConversationLists()
            : base("conversations", new Objects.Conversations.ConversationList())
        { }
        public override string BaseUrl
        {
            get { return Conversations.ConverstationsBaseUrl; }
        }
    }
}