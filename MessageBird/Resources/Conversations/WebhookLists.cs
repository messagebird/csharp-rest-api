using MessageBird.Objects.Conversations;

namespace MessageBird.Resources.Conversations
{
    public class WebhookLists : BaseLists<Webhook>
    {
        public WebhookLists()
            : base("messages", new WebhookList())
        { }
        
        public override string Endpoint => Conversations.ConverstationsEndpoint;
    }
}