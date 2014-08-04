using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace MessageBird.Resources
{
    class Hlr : Resource
    {
        public Hlr()
            : base("hlr")
        {
        }

        public Hlr(string id)
            : this()
        {
            Id = id;
        }

        public Hlr(Objects.Hlr hlr)
            : this()
        {
            Object = hlr;
        }

        private Objects.Hlr hlr;
        public override object Object
        {
            get
            {
                return hlr;
            }
            protected set
            {
                hlr = (Objects.Hlr)value;
                Id = hlr.Id;
            }
        }

        public override void Deserialize(string resource)
        {
            Object = JsonConvert.DeserializeObject<Objects.Hlr>(resource);
        }
    }
}
