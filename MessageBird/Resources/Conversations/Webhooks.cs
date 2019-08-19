namespace MessageBird.Resources.Conversations
{
    public class Webhooks : ConversationsResource
    {
        public Webhooks(Objects.Conversations.ConversationWebhook conversationWebhook, bool useWhatsAppSandbox = false) : base("webhooks", conversationWebhook, useWhatsAppSandbox) { }
        
        public Webhooks(bool useWhatsAppSandbox = false) : this(new Objects.Conversations.ConversationWebhook(), useWhatsAppSandbox) { }
        
    }
}