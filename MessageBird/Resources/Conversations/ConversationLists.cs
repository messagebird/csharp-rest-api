namespace MessageBird.Resources.Conversations
{
    public class ConversationLists : ConversationsBaseLists<Objects.Conversations.Conversation>
    {
        public ConversationLists(bool useWhatsAppSandbox = false)
            : base("conversations", new Objects.Conversations.ConversationList(), useWhatsAppSandbox)
        { }
    }
}