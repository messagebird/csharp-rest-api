using Newtonsoft.Json;
using MessageBird.Objects;

namespace MessageBird.Resources
{
    sealed class Messages : Resource
    {
        private Message message;
        public override object Object 
        {
            get
            {
                return message;
            }
            protected set
            {
                message = (Message)value;
                Id = message.Id;
            }
        }

        public Messages() : base("messages")
        {
        }

        public Messages(string id) : this()
        {
            Id = id;
        }

        public Messages(Message message) : this()
        {
           Object = message;
        }

        public override void Deserialize(string resource)
        {
            Object = JsonConvert.DeserializeObject<Message>(resource);
        }
    }
}
