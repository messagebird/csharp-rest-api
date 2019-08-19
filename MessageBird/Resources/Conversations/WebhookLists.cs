using MessageBird.Objects.Conversations;

namespace MessageBird.Resources.Conversations
{
    public class WebhookLists : ConversationsBaseLists<ConversationWebhook>
    {
        public WebhookLists(bool useWhatsAppSandbox)
            : base("webhooks", new ConversationWebhookList(), useWhatsAppSandbox)
        { }
        
    }
}