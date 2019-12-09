using System;
using MessageBird.Net;
using MessageBird.Objects;
using Newtonsoft.Json;

namespace MessageBird.Resources.Voice
{
    public class VoiceBaseResource<T> : Resource
    {
        public static string baseUrl = "https://voice.messagebird.com";

        public VoiceBaseResource(string name, IIdentifiable<string> attachedObject) :
            base(name, attachedObject)
        {
            //
        }

        public override string BaseUrl
        {
            get
            {
                return baseUrl;
            }
        }
    }
}