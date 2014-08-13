using MessageBird.Objects;

namespace MessageBird.Resources
{
    public sealed class Messages : Resource
    {
       public Messages(Message message)
            : base("messages", message)
        {
        }
    }
}
