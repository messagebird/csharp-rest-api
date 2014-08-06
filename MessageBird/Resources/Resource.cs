using System;
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

        protected Resource(string name)
        {
            Name = name;
        }
    }
}
