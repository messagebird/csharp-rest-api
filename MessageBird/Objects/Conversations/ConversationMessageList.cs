namespace MessageBird.Objects.Conversations
{
    public class ConversationMessageList : BaseList<ConversationMessage>

    {
        public string ConversationId { get; set; }
    }
}