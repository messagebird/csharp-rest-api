namespace MessageBird.Resources.Conversations
{
    public class Conversations : Resource
    {
        public Conversations(Objects.Conversations.Conversation conversation) : base("conversations", conversation) { }
        public Conversations() : this(new Objects.Conversations.Conversation()) { }
        public override string Endpoint => ConverstationsEndpoint;
    }
}