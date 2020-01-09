using System;
using MessageBird.Exceptions;
using MessageBird.Objects.Voice;
using Newtonsoft.Json;

namespace MessageBird.Resources.Voice
{
    public class Webhooks : VoiceBaseResource<Webhook>
    {
        public Webhooks(Objects.Voice.Webhook webhook) : base("webhooks", webhook) { }
        public Webhooks() : this(new Objects.Voice.Webhook()) { }

        public override void Deserialize(string resource)
        {
            try
            {
                Object = JsonConvert.DeserializeObject<VoiceResponse<Webhook>>(resource);
            }
            catch (JsonSerializationException e)
            {
                throw new ErrorException("Received response in an unexpected format!", e);
            }
        }

        /// <summary>
        /// Override the serialize function to remove the ID from the body of an update message (PUT)
        /// </summary>
        public override string Serialize()
        {    
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(new { ((Webhook)Object).url, ((Webhook)Object).token }, settings);
        }
    }
}
