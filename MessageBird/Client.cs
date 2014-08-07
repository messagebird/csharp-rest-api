using System.Net;
using MessageBird.Objects;
using MessageBird.Resources;
using MessageBird.Net;

namespace MessageBird
{
    public class Client
    {
        private readonly IRestClient restClient;

        private Client(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public static Client Create(IRestClient restClient)
        {
            return new Client(restClient);
        }

        public static Client CreateDefault(string accessKey, ICredentials proxyCredentials)
        {
            return new Client(new RestClient(accessKey, proxyCredentials));
        }

        public Message SendMessage(string originator, string body, long[] msisdns, MessageOptionalArguments optionalArguments = null)
        {
            Recipients recipients = new Recipients(msisdns);
            Message message = new Message(originator, body, recipients, optionalArguments);

            Messages messages = new Messages(message);
            Messages result = restClient.Create(messages);

            return result.Object as Message;
        }

        public Message ViewMessage(string id)
        {
            Messages messageToView = new Messages(id);
            Messages result = restClient.Retrieve(messageToView);

            return result.Object as Message;
        }

        public VoiceMessage SendVoiceMessage(string body, long[] msisdns, VoiceMessageOptionalArguments optionalArguments = null)
        {
            Recipients recipients = new Recipients(msisdns);
            VoiceMessage voiceMessage = new VoiceMessage(body, recipients, optionalArguments);

            VoiceMessages voiceMessages = new VoiceMessages(voiceMessage);
            VoiceMessages result = restClient.Create(voiceMessages);

            return result.Object as VoiceMessage;
        }

        public VoiceMessage ViewVoiceMessage(string id)
        {
            VoiceMessages voiceMessageToView = new VoiceMessages(id);
            VoiceMessages result = restClient.Retrieve(voiceMessageToView);

            return result.Object as VoiceMessage;
        }

        public Objects.Hlr RequestHlr(long msisdn, string reference)
        {
            Objects.Hlr hlr = new Objects.Hlr(msisdn, reference);
            Resources.Hlr hlrToRequest = new Resources.Hlr(hlr);

            Resources.Hlr result = restClient.Create(hlrToRequest);

            return result.Object as Objects.Hlr;
        }

        public Objects.Hlr ViewHlr(string id)
        {
            Objects.Hlr hlr = new Objects.Hlr(id);
            Resources.Hlr hlrToView = new Resources.Hlr(hlr);

            MessageBird.Resources.Hlr result = restClient.Retrieve(hlrToView);

            return result.Object as Objects.Hlr;
        }

        public Objects.Balance Balance()
        {
            Resources.Balance balance = new Resources.Balance();
            Resources.Balance result = restClient.Retrieve(balance);

            return result.Object as Objects.Balance;
        }
    }
}