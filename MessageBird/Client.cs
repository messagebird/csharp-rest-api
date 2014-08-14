using MessageBird.Net.ProxyConfigurationInjector;
using MessageBird.Objects;
using MessageBird.Resources;
using MessageBird.Net;
using MessageBird.Utilities;

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
            ParameterValidator.IsNotNull(restClient, "restClient");

            return new Client(restClient);
        }

        public static Client CreateDefault(string accessKey, IProxyConfigurationInjector proxyConfigurationInjector = null)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(accessKey, "accessKey");

            return new Client(new RestClient(accessKey, proxyConfigurationInjector));
        }

        public Message SendMessage(string originator, string body, long[] msisdns, MessageOptionalArguments optionalArguments = null)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(originator, "originator");
            ParameterValidator.IsNotNullOrWhiteSpace(body, "body");
            ParameterValidator.ContainsAtLeast(msisdns, 1, "msisdns");

            var recipients = new Recipients(msisdns);
            var message = new Message(originator, body, recipients, optionalArguments);

            var messages = new Messages(message);
            var result = restClient.Create(messages);

            return result.Object as Message;
        }

        public Message ViewMessage(string id)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(id, "id");

            var messageToView = new Messages(new Message(id));
            var result = restClient.Retrieve(messageToView);

            return result.Object as Message;
        }

        public VoiceMessage SendVoiceMessage(string body, long[] msisdns, VoiceMessageOptionalArguments optionalArguments = null)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(body, "body");
            ParameterValidator.ContainsAtLeast(msisdns, 1, "msisdns");

            var recipients = new Recipients(msisdns);
            var voiceMessage = new VoiceMessage(body, recipients, optionalArguments);
            var voiceMessages = new VoiceMessages(voiceMessage);
            var result = restClient.Create(voiceMessages);

            return result.Object as VoiceMessage;
        }

        public VoiceMessage ViewVoiceMessage(string id)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(id, "id");

            var voiceMessageToView = new VoiceMessages(new VoiceMessage(id));
            var result = restClient.Retrieve(voiceMessageToView);

            return result.Object as VoiceMessage;
        }

        public Objects.Hlr RequestHlr(long msisdn, string reference)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(reference, "reference");

            var hlrToRequest = new Resources.Hlr(new Objects.Hlr(msisdn, reference));
            var result = restClient.Create(hlrToRequest);

            return result.Object as Objects.Hlr;
        }

        public Objects.Hlr ViewHlr(string id)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(id, "id");

            var hlrToView = new Resources.Hlr(new Objects.Hlr(id));
            var result = restClient.Retrieve(hlrToView);

            return result.Object as Objects.Hlr;
        }

        public Objects.Balance Balance()
        {
            var balance = new Resources.Balance();
            var result = restClient.Retrieve(balance);

            return result.Object as Objects.Balance;
        }
    }
}