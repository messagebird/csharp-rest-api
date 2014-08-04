using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace MessageBird.Resources
{
    public class Hlr : IResource
    {
        private MessageBird.Objects.Hlr hlr;
        public MessageBird.Objects.Hlr HlrObject
        {
            get
            {
                return hlr;
            }
            set
            {
                if (id == null)
                {
                    id = value.Id;
                }
                hlr = value;
            }
        }
        public string Name { get { return "hlr"; } }
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
                    throw new InvalidResource("Requested an id of a hlr without an id!");
                }
            }
        }

        public Hlr()
        {
        }

        public Hlr(string id)
        {
            this.id = id;
        }

        public Hlr(MessageBird.Objects.Hlr hlr)
        {
            HlrObject = hlr;
            if (hlr.Id != null)
            {
                id = hlr.Id;
            }
        }

        public void Deserialize(string resource)
        {
            HlrObject = JsonConvert.DeserializeObject<MessageBird.Objects.Hlr>(resource);
        }

        public string Serialize()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(HlrObject, settings);
        }
    }
}
