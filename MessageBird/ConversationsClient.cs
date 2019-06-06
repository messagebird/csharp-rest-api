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
            
            restClient.Update(resource);

            return resource.Object as Conversation;
        }

        public ConversationMessageList ListConversationMessages(string conversationId, int limit = 20, int offset = 0)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(conversationId, "conversationId");
            
            var resource = new MessageLists();

            var list = (ConversationMessageList) resource.Object;
            list.Limit = limit;
            list.Offset = offset;
            list.ConversationId = conversationId;

            restClient.Retrieve(resource);

            return resource.Object as ConversationMessageList;
        }

        public ConversationMessage ViewConversationMessage(string id)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(id, "id");
            
            var resource = new Messages(new ConversationMessage {Id = id});
            restClient.Retrieve(resource);

            return resource.Object as ConversationMessage;
        }

        public ConversationMessage SendConversationMessage(string conversationId, ConversationMessageSendRequest conversationMessageRequest)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(conversationId, "conversationId");
            
            conversationMessageRequest.ConversationId = conversationId;
            var resource = new MessageSend(conversationMessageRequest);
            restClient.Create(resource);

            return resource.Object as ConversationMessage;
        }

        public ConversationWebhookList ListConversationWebhooks(int limit = 20, int offset = 0)
        {
            var resource = new WebhookLists();

            var list = (ConversationWebhookList) resource.Object;
            list.Limit = limit;
            list.Offset = offset;

            restClient.Retrieve(resource);

            return resource.Object as ConversationWebhookList;
        }

        public ConversationWebhook ViewConversationWebhook(string id)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(id, "id");
            
            var resource = new Webhooks(new ConversationWebhook {Id = id});
            restClient.Retrieve(resource);

            return resource.Object as ConversationWebhook;
        }

        public ConversationWebhook CreateConversationWebhook(ConversationWebhook conversationWebhook)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(conversationWebhook.ChannelId, "channelId");
            ParameterValidator.IsNotNullOrWhiteSpace(conversationWebhook.Url, "url");
            ParameterValidator.ContainsAtLeast(conversationWebhook.Events.ToArray(), 1, "events");
            
            var resource = new Webhooks(conversationWebhook);
            restClient.Create(resource);

            return resource.Object as ConversationWebhook;
        }

        public void DeleteConversationWebhook(string id)
        {
            ParameterValidator.IsNotNullOrWhiteSpace(id, "id");
            
            var resource = new Webhooks(new ConversationWebhook {Id = id});
            restClient.Delete(resource);
        }
    }
}