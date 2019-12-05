﻿using MessageBird.Objects;
using MessageBird.Objects.Voice;
using Newtonsoft.Json;

namespace MessageBird.Resources.Voice
{
    public class VoiceCallFlowsResource : Resource
    {
        public static string VoiceCallFlowsBaseUrl = "https://voice.messagebird.com";

        public VoiceCallFlowsResource(string name, IIdentifiable<string> attachedObject) :
            base(name, attachedObject)
        {
        }

        public override string BaseUrl
        {
            get
            {
                return VoiceCallFlowsBaseUrl;
            }
        }

        public override string Serialize()
        {
            var requestObject = ((VoiceCallFlow)Object).ToRequestObject();

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(requestObject, settings);
        }
    }
}