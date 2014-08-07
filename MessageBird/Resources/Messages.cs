using Newtonsoft.Json;
using MessageBird.Objects;

namespace MessageBird.Resources
{
    sealed class Messages : Resource
    {
       public Messages(Message message)
            : base("messages", message)
        {
        }
    }
}
