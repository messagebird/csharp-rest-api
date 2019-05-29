namespace MessageBird.Resources.Conversations
{
    public class Messages : Resource
    {
        public Messages(Objects.Conversations.Message message) : base("messages", message) { }
        public Messages() : this(new Objects.Conversations.Message()) { }
    }
}