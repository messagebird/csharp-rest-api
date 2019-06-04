namespace MessageBird.Objects.Conversations
{
    public class MessageList : BaseList<Message>

    {
        public string ConversationId { get; set; }
    }
}