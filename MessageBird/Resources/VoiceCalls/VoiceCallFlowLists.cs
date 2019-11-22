namespace MessageBird.Resources.VoiceCalls
{
    public class VoiceCallFlowLists : VoiceCallFlowsBaseLists<Objects.VoiceCalls.VoiceCallFlow>
    {
        public VoiceCallFlowLists()
            : base("call-flows", new Objects.VoiceCalls.VoiceCallFlowList())
        { }
    }
}