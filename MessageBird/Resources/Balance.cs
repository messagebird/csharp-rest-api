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

        public override bool HasId
        {
            get
            {
                return false;
            }
        }
    }
}
