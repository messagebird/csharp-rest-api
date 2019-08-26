using System;
using MessageBird.Objects.Conversations;
using Newtonsoft.Json;

namespace MessageBird.Resources.Conversations
{
    public class MessageSend : ConversationsResource
    {
        public ConversationMessageSendRequest Request { get; protected set; }

        public MessageSend(ConversationMessageSendRequest request, bool useWhatsAppSandbox = false)
            : base("messages", new ConversationMessage(), useWhatsAppSandbox)
        {
            Request = request;
        }

        public override string Serialize()
        {
            var settings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
            return JsonConvert.SerializeObject(Request, settings);
        }
        
        public override string Uri
        {
            get { return String.Format("conversations/{0}/{1}", Request.ConversationId, Name); }
        }
    }
}