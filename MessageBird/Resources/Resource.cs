using System;
using MessageBird.Exceptions;
using MessageBird.Objects;
using Newtonsoft.Json;

namespace MessageBird.Resources
{
    public abstract class Resource
    {
        public string Id
        {
            get
            {
                if (HasId)
                {
                   return Object.Id;
                }
                throw new ErrorException(String.Format("Resource {0} has no id", Name));
            }
        }

        public IIdentifiable<string> Object { get; protected set; }

        public bool HasId
        {
            get { return (Object != null) && !String.IsNullOrEmpty(Object.Id); }
        }

        public string Name { get; private set; }

        public virtual void Deserialize(string resource)
        {
            if (Object == null)
            {
                throw new ErrorException("Invalid resource, has no attached object", new NullReferenceException());
            }

            try
            {
                JsonConvert.PopulateObject(resource, Object);
            }
            catch (JsonSerializationException e)
            {
                throw new ErrorException("Received response in an unexpected format!", e);
            }
        }

        public virtual string Serialize()
        {
            var settings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
            return JsonConvert.SerializeObject(Object, settings);
        }

        protected Resource(string name, IIdentifiable<string> attachedObject)
        {
            Name = name;
            Object = attachedObject;
        }
    }
}
