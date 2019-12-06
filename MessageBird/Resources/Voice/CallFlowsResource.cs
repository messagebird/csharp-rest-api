using MessageBird.Objects;
using MessageBird.Objects.Voice;
using Newtonsoft.Json;

namespace MessageBird.Resources.Voice
{
    public class CallFlowsResource : Resource
    {
        public static string CallFlowsBaseUrl = "https://voice.messagebird.com";

        public CallFlowsResource(string name, IIdentifiable<string> attachedObject) :
            base(name, attachedObject)
        {
        }

        public override string BaseUrl
        {
            get
            {
                return CallFlowsBaseUrl;
            }
        }

        public override string Serialize()
        {
            var requestObject = ((CallFlow)Object).ToRequestObject();

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(requestObject, settings);
        }
    }
}