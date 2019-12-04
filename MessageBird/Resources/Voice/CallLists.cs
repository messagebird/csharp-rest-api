using MessageBird.Objects.Voice;

namespace MessageBird.Resources.Voice
{
    public class CallLists : CallBaseLists<Objects.Voice.Call>
    {
        public CallLists()
            : base("calls", new CallList())
        { }
    }
}


