using Newtonsoft.Json;
using MessageBird.Objects;

namespace MessageBird.Resources
{
    sealed class Messages : Resource
    {
       
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
    }
}
