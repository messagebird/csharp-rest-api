using System;
using MessageBird.Exceptions;
using MessageBird.Net;
using Newtonsoft.Json;

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

        public override void Deserialize(string resource)
        {
            try
            {
                Object = JsonConvert.DeserializeObject<Objects.VoiceCalls.VoiceCallFlowResponse>(resource);
            }
            catch (JsonSerializationException e)
            {
                throw new ErrorException("Received response in an unexpected format!", e);
            }
        }
    }
}