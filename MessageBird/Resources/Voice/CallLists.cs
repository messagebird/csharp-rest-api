using MessageBird.Objects.Voice;

namespace MessageBird.Resources.Voice
{
    public class CallLists : CallBaseLists
    {
        public CallLists()
            : base("calls", new CallList())
        { }
    }
}