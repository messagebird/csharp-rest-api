using MessageBird.Objects.Conversations;

namespace MessageBird.Resources.Conversations
{
    public class WebhookLists : BaseLists<Webhook>
    {
        public WebhookLists()
            : base("webhooks", new WebhookList())
        { }
        
        public override string Endpoint => ConverstationsEndpoint;
    }
}