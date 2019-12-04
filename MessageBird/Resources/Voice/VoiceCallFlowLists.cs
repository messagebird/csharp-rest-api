using MessageBird.Objects.Voice;

namespace MessageBird.Resources.Voice
{
    public class VoiceCallFlowLists : VoiceCallFlowsBaseLists
    {
        public VoiceCallFlowLists()
            : base("call-flows", new VoiceCallFlowList())
        { }
    }
}