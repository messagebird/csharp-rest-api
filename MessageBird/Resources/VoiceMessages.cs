using Newtonsoft.Json;
using MessageBird.Objects;

namespace MessageBird.Resources
{
    sealed class VoiceMessages : Resource
    {
        public VoiceMessages(VoiceMessage voiceMessage) :
            base("voicemessages", voiceMessage)
        {
            Object = voiceMessage;
        }
    }
}
