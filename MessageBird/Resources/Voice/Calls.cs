using System;
using MessageBird.Exceptions;
using MessageBird.Objects.Voice;
using Newtonsoft.Json;


namespace MessageBird.Resources.Voice
{
    public class Calls : VoiceBaseResource<Call>
    {
        public Calls(Objects.Voice.Call call) : base("calls", call) { }
        public Calls() : this(new Objects.Voice.Call()) { }

        public override void Deserialize(string resource)
        {
            try
            {
                Object = JsonConvert.DeserializeObject<VoiceResponse<Call>>(resource);
            }
            catch (JsonSerializationException e)
            {
                throw new ErrorException("Received response in an unexpected format!", e);
            }
        }
    }
}