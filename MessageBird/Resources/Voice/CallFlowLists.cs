using MessageBird.Objects.Voice;

namespace MessageBird.Resources.Voice
{
    public class CallFlowLists : VoiceBaseLists<CallFlow>
    {
        public CallFlowLists()
            : base("call-flows", new CallFlowList())
        { }

        public CallFlowLists(Objects.Voice.CallFlowList list) : base("call-flows", list) { }
    }
}