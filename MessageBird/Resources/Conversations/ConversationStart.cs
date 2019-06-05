using System;
using MessageBird.Objects.Conversations;
using Newtonsoft.Json;

namespace MessageBird.Resources.Conversations
{
    public class ConversationStart : Resource
    {
        public ConversationStartRequest Request { get; protected set; }

        public ConversationStart(ConversationStartRequest request)
            : base("conversations", new Conversation())
        {
            Request = request;
        }

        public override string Uri
        {
            get { return string.Format("{0}/start", Name); }
        }

        public override string Serialize()
        {
            var settings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
            return JsonConvert.SerializeObject(Request, settings);
        }
        public override string BaseUrl
        {
            get { return Conversations.ConverstationsBaseUrl; }
        }
    }
}