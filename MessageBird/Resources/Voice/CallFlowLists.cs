using MessageBird.Objects.Voice;

namespace MessageBird.Resources.Voice
{
    public class CallFlowLists : VoiceBaseLists<CallFlow>
    {
        public CallFlowLists()
            : base("call-flows", new CallFlowList())
        { }
    }
}