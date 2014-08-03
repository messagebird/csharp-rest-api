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

    public class Messages : IResource
    {
        private Message message;
        public Message Message 
        {
            get
            {
                return message;
            }
            set
            {
                if (id == null)
                {
                    id = value.Id;
                }
                message = value;
            }
        }
        public string Name { get { return "messages"; } }
        private string id;
        public string Id 
        {
            get
            {
                if (id != null)
                {
                    return id;
                }
                else
                {
                    throw new InvalidResource("Requested an id of a message without an id!");
                }
            }
        }

        public Messages()
        {
        }

        public Messages(string id)
        {
            this.id = id;
        }

        public Messages(Message message)
        {
            Message = message;
            if (message.Id != null)
            {
                id = message.Id;
            }
        }

        public void Deserialize(string resource)
        {
            Message = JsonConvert.DeserializeObject<Message>(resource);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(Message);
        }
    }
}
