namespace MessageBird.Resources.Conversations
{
    public class Webhooks : Resource
    {
        public Webhooks(Objects.Conversations.ConversationWebhook conversationWebhook) : base("webhooks", conversationWebhook) { }
        
        public Webhooks() : this(new Objects.Conversations.ConversationWebhook()) { }
        
        public override string BaseUrl
        {
            get { return Conversations.ConverstationsBaseUrl; }
        }
    }
}