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
                throw CreateABalanceHasNoIdErrorException();
            }
            protected set
            {
                throw CreateABalanceHasNoIdErrorException();
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

        private static ErrorException CreateABalanceHasNoIdErrorException()
        {
            return new ErrorException("A balance has no id", null);
        }
    }
}
