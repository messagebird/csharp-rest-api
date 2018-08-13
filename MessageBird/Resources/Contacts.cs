using Newtonsoft.Json;

namespace MessageBird.Resources
{
    public class Contacts : Resource
    {
        public Contacts(Objects.Contact contact)
            : base("contacts", contact)
        {
        }

        public Contacts()
            : this(new Objects.Contact())
        {

        }
        
        public override string Serialize()
        {
            var requestObject = ((Objects.Contact)Object).ToRequestObject();

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(requestObject, settings);
        }
    }
}
