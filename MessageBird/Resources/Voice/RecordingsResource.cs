using MessageBird.Objects;

namespace MessageBird.Resources.Voice
{
    public class RecordingsResource : Resource
    {
        public static string RecordingsBaseUrl = "https://voice.messagebird.com";

        public RecordingsResource(string name, IIdentifiable<string> attachedObject) :
            base(name, attachedObject)
        {
        }

        public override string BaseUrl
        {
            get
            {
                return RecordingsBaseUrl;
            }
        }
    }
}