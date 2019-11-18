using MessageBird.Net;

namespace MessageBird.Resources.VoiceCalls
{
    public class VoiceCallFlows : VoiceCallFlowsResource
    {
        public VoiceCallFlows(Objects.VoiceCalls.VoiceCallFlow voiceCallFlow) : base("call-flows", voiceCallFlow) { }
        public VoiceCallFlows() : this(new Objects.VoiceCalls.VoiceCallFlow()) { }
        public override UpdateMode UpdateMode
        {
            get { return UpdateMode.Put; }
        }
    }
}