using MessageBird.Objects.Voice;

namespace MessageBird.Resources.Voice
{
    public class CallLists : VoiceBaseLists<Call>
    {
        public CallLists()
            : base("calls", new CallList())
        { }
    }
}