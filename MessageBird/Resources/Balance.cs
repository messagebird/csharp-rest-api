using Newtonsoft.Json;
using MessageBird.Exceptions;

namespace MessageBird.Resources
{
    class Balance : Resource
    {

        public Balance()
            : base("balance")
        {

        }

        public override string Id
        {
            get
            {
                throw new ErrorException("A balance has no id");
            }
            protected set
            {
                throw new ErrorException("A balance has no id");
            }
        }

        public override bool HasId
        {
            get
            {
                return false;
            }
        }

        public override void Deserialize(string resource)
        {
            Object = JsonConvert.DeserializeObject<MessageBird.Objects.Balance>(resource);
        }
    }
}
