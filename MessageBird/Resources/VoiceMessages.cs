using Newtonsoft.Json;
using MessageBird.Objects;

namespace MessageBird.Resources
{
    sealed class VoiceMessages : Resource
    {
        public VoiceMessages() : base("voicemessages")
        {
        }

        public VoiceMessages(string id) : this()
        {
            Id = id;
        }

        public VoiceMessages(VoiceMessage voiceMessage) : this ()
        {
            Object = voiceMessage;
        }
    }
}
