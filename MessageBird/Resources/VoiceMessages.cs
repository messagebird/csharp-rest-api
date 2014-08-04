using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MessageBird.Objects;
using Newtonsoft.Json;

namespace MessageBird.Resources
{
    public class VoiceMessages : IResource
    {
        private VoiceMessage voiceMessage;
        public VoiceMessage VoiceMessage
        {
            get
            {
                return voiceMessage;
            }
            set
            {
                if (id == null)
                {
                    id = value.Id;
                }
                voiceMessage = value;
            }
        }
        public string Name { get { return "voicemessages"; } }
        private string id;
        public string Id
        {
            get
            {
                if (id != null)
                {
                    return id;
                }
                else
                {
                    throw new InvalidResource("Requested an id of a voice message without an id!");
                }
            }
        }

        public VoiceMessages()
        {
        }

        public VoiceMessages(string id)
        {
            this.id = id;
        }

        public VoiceMessages(VoiceMessage voiceMessage)
        {
            VoiceMessage = voiceMessage;
            if (voiceMessage.Id != null)
            {
                id = voiceMessage.Id;
            }
        }

        public void Deserialize(string resource)
        {
            VoiceMessage = JsonConvert.DeserializeObject<VoiceMessage>(resource);
        }

        public string Serialize()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(VoiceMessage, settings);
        }
    }
}
