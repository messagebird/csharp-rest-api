using System;
using MessageBird.Objects;
using MessageBird.Resources;
using MessageBird.Net;

namespace MessageBird
{
    public class Client
    {
        private IRestClient restClient;
        private Client(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public static Client Create(IRestClient restClient)
        {
            return new Client(restClient);
        }

        public static Client CreateDefault(string accessKey)
        {
            return new Client(new RestClient(accessKey));
        }

        public Message SendMessage(Message message)
        {
            Messages messageToSend = new Messages(message);
            Messages result = (Messages)restClient.Create(messageToSend);

            return result.Message;
        }

        public Message SendMessage(string originator, string body, long[] msisdns)
        {
            Recipients recipients = new Recipients(msisdns);
            Message message = new Message(originator, body, recipients);

            Messages messages = new Messages(message);
            Messages result = (Messages)restClient.Create(messages);

            return result.Message;
        }

        public Message ViewMessage(string id)
        {
            Messages messageToView = new Messages(id);
            Messages result = (Messages)restClient.Retrieve(messageToView);

            return result.Message;
        }
    }
}