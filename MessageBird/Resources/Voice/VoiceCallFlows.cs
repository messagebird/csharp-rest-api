using MessageBird.Exceptions;
using MessageBird.Net;
using MessageBird.Objects.Voice;
using Newtonsoft.Json;

namespace MessageBird.Resources.Voice
{
    public class VoiceCallFlows : VoiceCallFlowsResource
    {
        public VoiceCallFlows(VoiceCallFlow voiceCallFlow) : base("call-flows", voiceCallFlow) { }
        public VoiceCallFlows() : this(new VoiceCallFlow()) { }
        
        public override UpdateMode UpdateMode
        {
            get { return UpdateMode.Put; }
        }

        public override void Deserialize(string resource)
        {
            try
            {
                Object = JsonConvert.DeserializeObject<VoiceCallFlowResponse>(resource);
            }
            catch (JsonSerializationException e)
            {
                throw new ErrorException("Received response in an unexpected format!", e);
            }
        }
    }
}