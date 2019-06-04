namespace MessageBird.Resources.Conversations
{
    public class Webhooks : Resource
    {
        public Webhooks(Objects.Conversations.Webhook webhook) : base("webhooks", webhook) { }
        
        public Webhooks() : this(new Objects.Conversations.Webhook()) { }
        
        public override string Endpoint => Conversations.ConverstationsEndpoint;
    }
}