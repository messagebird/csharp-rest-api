using MessageBird.Exceptions;
using MessageBird.Net;
using MessageBird.Objects.Voice;
using Newtonsoft.Json;

namespace MessageBird.Resources.Voice
{
    public class CallFlows : CallFlowsResource
    {
        public CallFlows(CallFlow callFlow) : base("call-flows", callFlow) { }
        public CallFlows() : this(new CallFlow()) { }
        
        public override UpdateMode UpdateMode
        {
            get { return UpdateMode.Put; }
        }

        public override void Deserialize(string resource)
        {
            try
            {
                Object = JsonConvert.DeserializeObject<CallFlowResponse>(resource);
            }
            catch (JsonSerializationException e)
            {
                throw new ErrorException("Received response in an unexpected format!", e);
            }
        }
    }
}