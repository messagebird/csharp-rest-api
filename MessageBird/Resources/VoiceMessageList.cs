using System.Text;

namespace MessageBird.Resources
{
    public class VoiceMessageLists : BaseLists<Objects.VoiceMessage>
    {
        public VoiceMessageLists() : base("voicemessages", new Objects.VoiceMessageList()) { }

        public VoiceMessageLists(Objects.VoiceMessageList voiceMessageList) : base("voicemessages", voiceMessageList) { }

        public override string QueryString
        {
            get
            {
                var baseList = (Objects.VoiceMessageList)Object;

                var builder = new StringBuilder();

                if (!string.IsNullOrEmpty(base.QueryString))
                {
                    builder.AppendFormat("{0}", base.QueryString);
                }

                return builder.ToString();
            }
        }
    }
}