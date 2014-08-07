using System;
using MessageBird.Exceptions;
using MessageBird.Objects;
using Newtonsoft.Json;

namespace MessageBird.Resources
{
    public abstract class Resource
    {
        private string _id;

        public string Id
        {
            get
            {
                if (HasId)
                {
                    if (!String.IsNullOrEmpty(_id))
                    {
                        return _id;
                    }

                    if (Object != null)
                    {
                        return Object.Id;
                    }
                    throw new ErrorException(String.Format("Expected an id for resource {0}", Name));
                }
                else
                {
                    throw new ErrorException(String.Format("Resource {0} has no id", Name));
                }
            }
            protected set { _id = value; }
        }

        private IIdentifiable<string> _object;
        public IIdentifiable<string> Object
        {
            get { return _object; }
            protected set
            {
                _object = value;
                Id = _object.Id;
            }
        } 
        public virtual bool HasId { get { return true; } }

        public string Name { get; private set; }

        public virtual void Deserialize(string resource)
        {
            JsonConvert.PopulateObject(resource, Object);
        }

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
