using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MessageBird.Objects;
using Newtonsoft.Json;

namespace MessageBird.Resources
{
    class VoiceMessages : Resource
    {
        private VoiceMessage voiceMessage;
        public override object Object
        {
            get
            {
                return voiceMessage;
            }
            protected set
            {
                voiceMessage = (VoiceMessage)value;
                Id = voiceMessage.Id;
            }
        }

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

        public override void Deserialize(string resource)
        {
            Object = JsonConvert.DeserializeObject<VoiceMessage>(resource);
        }
    }
}
