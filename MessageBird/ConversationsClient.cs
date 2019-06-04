using MessageBird.Net;
using MessageBird.Objects.Conversations;
using MessageBird.Resources.Conversations;
using MessageBird.Utilities;

namespace MessageBird
{
    public partial class Client
    {
        public ConversationList ListConversations(int limit = 20, int offset = 0)
        {
            var resource = new ConversationLists();

            var list = (ConversationList) resource.Object;
            list.Limit = limit;
            list.Offset = offset;

            restClient.Retrieve(resource);

            return resource.Object as ConversationList;
        }

        public Conversation ViewConversation(string id)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(id, "id");
            var resource = new Conversations(new Conversation {Id = id});
            restClient.Retrieve(resource);

            return resource.Object as Conversation;
        }

        public Conversation StartConversation(ConversationStartRequest startRequest)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(startRequest.To, "to");
            ParameterValidator.IsNotNullOrWhiteSpace(startRequest.ChannelId, "channelId");
            ParameterValidator.IsNotNull(startRequest.Type, "type");
            ParameterValidator.IsNotNull(startRequest.Content, "content");
            var resource = new ConversationStart(startRequest);
            restClient.Create(resource);

            return resource.Object as Conversation;
        }

        public Conversation UpdateConversation(string id, Conversation conversation)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(id, "id");
            conversation.Id = id;
            var resource = new Conversations(conversation);

            RestClientOptions.UpdateMode = UpdateMode.Patch;
            restClient.Update(resource);

            return resource.Object as Conversation;
        }

        public MessageList ListConversationMessages(string conversationId, int limit = 20, int offset = 0)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(conversationId, "conversationId");
            var resource = new MessageLists();

            var list = (MessageList) resource.Object;
            list.Limit = limit;
            list.Offset = offset;
            list.ConversationId = conversationId;

            restClient.Retrieve(resource);

            return resource.Object as MessageList;
        }

        public Message ViewConversationMessage(string id)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(id, "id");
            var resource = new Messages(new Message {Id = id});
            restClient.Retrieve(resource);

            return resource.Object as Message;
        }

        public Message SendConversationMessage(string conversationId, MessageSendRequest messageRequest)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(conversationId, "conversationId");
            messageRequest.ConversationId = conversationId;
            var resource = new MessageSend(messageRequest);
            restClient.Create(resource);

            return resource.Object as Message;
        }

        public WebhookList ListConversationWebhooks(int limit = 20, int offset = 0)
        {
            var resource = new WebhookLists();

            var list = (WebhookList) resource.Object;
            list.Limit = limit;
            list.Offset = offset;

            restClient.Retrieve(resource);

            return resource.Object as WebhookList;
        }

        public Webhook ViewConversationWebhook(string id)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(id, "id");
            var resource = new Webhooks(new Webhook {Id = id});
            restClient.Retrieve(resource);

            return resource.Object as Webhook;
        }

        public Webhook CreateConversationWebhook(Webhook webhook)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(webhook.ChannelId, "channelId");
            ParameterValidator.IsNotNullOrWhiteSpace(webhook.Url, "url");
            ParameterValidator.ContainsAtLeast(webhook.Events.ToArray(), 1, "events");
            var resource = new Webhooks(webhook);
            restClient.Create(resource);

            return resource.Object as Webhook;
        }

        public void DeleteConversationWebhook(string id)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(id, "id");
            var resource = new Webhooks(new Webhook {Id = id});
            restClient.Delete(resource);
        }
    }
}