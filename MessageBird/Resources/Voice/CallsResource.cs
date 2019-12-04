using System;
using MessageBird.Net;
using MessageBird.Objects;
using Newtonsoft.Json;

namespace MessageBird.Resources.Voice
{
    public class CallsResource : Resource
    {
        public static string CallBaseUrl = "https://voice.messagebird.com";

        public CallsResource(string name, IIdentifiable<string> attachedObject) :
            base(name, attachedObject)
        {
            //
        }

        public override string BaseUrl
        {
            get
            {
                return CallBaseUrl;
            }
        }
    }
}