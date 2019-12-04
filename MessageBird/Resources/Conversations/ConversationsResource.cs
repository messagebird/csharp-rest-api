using MessageBird.Objects;

namespace MessageBird.Resources.Conversations
{
    public class ConversationsResource : Resource
    {
        public static string ConverstationsBaseUrl = "https://conversations.messagebird.com/v1";
        public static string ConverstationsWhatsAppSandboxBaseUrl = "https://whatsapp-sandbox.messagebird.com/v1";

        protected bool useConversationsWhatsappSandbox;

        public ConversationsResource(string name, IIdentifiable<string> attachedObject, bool useWhatsappSandbox) :
            base(name, attachedObject) 
        {
            this.useConversationsWhatsappSandbox = useWhatsappSandbox;
        }

        public override string BaseUrl
        {
            get
            {
                if (this.useConversationsWhatsappSandbox)
                {
                    return ConverstationsWhatsAppSandboxBaseUrl;

                }
                else
                {
                    return ConverstationsBaseUrl;
                }
            }
        }
    }
}