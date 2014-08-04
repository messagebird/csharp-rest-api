using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using Newtonsoft.Json;

namespace MessageBird.Resources
{
    public abstract class Resource
    {
        public virtual string Id { get; protected set; }
        public virtual object Object { get; protected set; } 
        public virtual bool HasId { get { return !String.IsNullOrEmpty(Id); } }

        public string Name { get; private set; }

        public abstract void Deserialize(string resource);

        public virtual string Serialize()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(Object, settings);
        }

        public Resource(string name)
        {
            Name = name;
        }
    }
}
