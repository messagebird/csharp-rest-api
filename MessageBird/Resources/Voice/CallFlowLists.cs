using MessageBird.Objects.Voice;

namespace MessageBird.Resources.Voice
{
    public class CallFlowLists : CallFlowsBaseLists
    {
        public CallFlowLists()
            : base("call-flows", new CallFlowList())
        { }
    }
}