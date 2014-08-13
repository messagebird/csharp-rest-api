using MessageBird.Objects;

namespace MessageBird.Resources
{
    public sealed class VoiceMessages : Resource
    {
        public VoiceMessages(VoiceMessage voiceMessage) :
            base("voicemessages", voiceMessage)
        {
            Object = voiceMessage;
        }
    }
}
