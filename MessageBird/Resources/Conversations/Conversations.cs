using MessageBird.Net;

namespace MessageBird.Resources.Conversations
{
    public class Conversations : Resource
    {
        public Conversations(Objects.Conversations.Conversation conversation) : base("conversations", conversation) { }
        public Conversations() : this(new Objects.Conversations.Conversation()) { }
        public override string BaseUrl
        {
            get { return ConverstationsBaseUrl; }
        }

        public override UpdateMode UpdateMode
        {
            get { return UpdateMode.Patch; }
        }
    }
}