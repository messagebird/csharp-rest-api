using System;
using MessageBird.Objects.Conversations;
using Newtonsoft.Json;

namespace MessageBird.Resources.Conversations
{
    public class MessageSend : Resource
    {
        public ConversationMessageSendRequest Request { get; protected set; }

        public MessageSend(ConversationMessageSendRequest request)
            : base("messages", new ConversationMessage())
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

        public override string BaseUrl
        {
            get { return Conversations.ConverstationsBaseUrl; }
        }
    }
}