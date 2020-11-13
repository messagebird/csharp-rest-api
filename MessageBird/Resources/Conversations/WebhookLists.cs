using MessageBird.Objects.Conversations;

namespace MessageBird.Resources.Conversations
{
    public class WebhookLists : BaseLists<ConversationWebhook>
    {
        public WebhookLists()
            : base("webhooks", new ConversationWebhookList())
        { }
        
        public override string BaseUrl
        {
            get { return ConverstationsBaseUrl; }
        }
    }
}