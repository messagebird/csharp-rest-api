using MessageBird.Objects;
using Newtonsoft.Json;
using System;

namespace MessageBird.Resources
{
    public class InvalidResource : Exception 
    {
        public string Reason {get; set;}
        public InvalidResource(string reason)
        {
            Reason = reason;
        }
    }

    public class Messages : Resource
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
