using Newtonsoft.Json;

namespace MessageBird.Resources
{
    sealed class Hlr : Resource
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

    }
}
