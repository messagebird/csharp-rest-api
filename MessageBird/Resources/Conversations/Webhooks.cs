namespace MessageBird.Resources.Conversations
{
    public class Webhooks : ConversationsResource
    {
        public Webhooks(Objects.Conversations.ConversationWebhook conversationWebhook, bool useWhatsAppSandbox) : base("webhooks", conversationWebhook, useWhatsAppSandbox) { }
        
        public Webhooks(bool useWhatsAppSandbox) : this(new Objects.Conversations.ConversationWebhook(), useWhatsAppSandbox) { }
        
    }
}