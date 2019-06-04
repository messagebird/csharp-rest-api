using System;
using MessageBird.Objects.Conversations;
using Newtonsoft.Json;

namespace MessageBird.Resources.Conversations
{
    public class MessageSend : Resource
    {
        public MessageSendRequest Request { get; protected set; }

        public MessageSend(MessageSendRequest request)
            : base("messages", new Message())
        {
            Request = request;
        }

        public override string Serialize()
        {
            var settings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
            return JsonConvert.SerializeObject(Request, settings);
        }
        
        public override string Uri => String.Format("conversations/{0}/{1}", Request.ConversationId, Name);
        
        public override string Endpoint => Conversations.ConverstationsEndpoint;
    }
}