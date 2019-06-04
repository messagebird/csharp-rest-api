using System;
using System.Collections.Generic;
using System.IO;
using MessageBird;
using MessageBird.Objects.Conversations;
using MessageBird.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageBirdTests.Resources
{
    [TestClass]
    public class ConversationTest
    {
        private const string ConvId = "2e15efafec384e1c82e9842075e87beb";
        private const string MsgId = "13a97a5023944648b8e5e61248c40da8";
        private const string WebhookId = "985ae50937a94c64b392531ea87a0263";

        [TestMethod]
        public void Start()
        {
            var restClient = MockRestClient
                .ThatExpects(
                    @"{""to"":""+31612345678"",""type"":""text"",""content"":{""text"":""Hello!""},""channelId"":""619747f69cf940a98fb443140ce9aed2""}")
                .AndReturns(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Responses",
                    "ConversationView.json")))
                .FromEndpoint("POST", "conversations/start", Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            var req = new ConversationStartRequest
            {
                ChannelId = "619747f69cf940a98fb443140ce9aed2",
                Content = new Content
                {
                    Text = "Hello!"
                },
                Type = ContentType.Text,
                To = "+31612345678"
            };
            var conversation = client.StartConversation(req);
            restClient.Verify();

            Assert.AreEqual(req.ChannelId, conversation.LastUsedChannelId);
            Assert.AreEqual("31612345678", conversation.Contact.Msisdn);
            Assert.AreEqual(2, conversation.Channels.Count);
        }

        [TestMethod]
        public void List()
        {
            var restClient = MockRestClient
                .ThatReturns(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Responses",
                    "ConversationList.json")))
                .FromEndpoint("GET", "conversations?limit=20&offset=0", Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            var groups = client.ListConversations();
            restClient.Verify();

            Assert.AreEqual(2, groups.TotalCount);
            Assert.AreEqual(10, groups.Items[0].Messages.TotalCount);
        }

        [TestMethod]
        public void ListPagination()
        {
            var restClient = MockRestClient
                .ThatReturns("{}")
                .FromEndpoint("GET", "conversations?limit=50&offset=10", Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            client.ListConversations(50, 10);
            restClient.Verify();
        }

        [TestMethod]
        public void View()
        {
            var restClient = MockRestClient
                .ThatReturns(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Responses",
                    "ConversationView.json")))
                .FromEndpoint("GET", "conversations/2e15efafec384e1c82e9842075e87beb", Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            var conversation = client.ViewConversation(ConvId);
            restClient.Verify();

            Assert.AreEqual(ConvId, conversation.Id);
            Assert.AreEqual("31612345678", conversation.Contact.Msisdn);
            Assert.AreEqual(2, conversation.Channels.Count);
        }

        [TestMethod]
        public void Update()
        {
            var restClient = MockRestClient
                .ThatExpects(@"{""id"":""2e15efafec384e1c82e9842075e87beb"",""status"": ""archived""}")
                .AndReturns(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Responses",
                    "ConversationView.json")))
                .FromEndpoint("PATCH", $"conversations/{ConvId}", Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            var conversation =
                client.UpdateConversation(ConvId, new Conversation {Status = ConversationStatus.Archived});
            restClient.Verify();

            Assert.AreEqual(ConvId, conversation.Id);
            Assert.AreEqual(ConversationStatus.Archived, conversation.Status);
        }

        [TestMethod]
        public void ListMessages()
        {
            var restClient = MockRestClient
                .ThatReturns(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Responses",
                    "ConversationMessagesList.json")))
                .FromEndpoint("GET", $"conversations/{ConvId}/messages?limit=20&offset=0",
                    Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            var messages = client.ListConversationMessages(ConvId);
            restClient.Verify();

            Assert.AreEqual(24, messages.TotalCount);
            Assert.AreEqual(2, messages.Count);
            Assert.AreEqual(ConvId, messages.Items[0].ConversationId);
        }

        [TestMethod]
        public void ListMessagesPagination()
        {
            var restClient = MockRestClient
                .ThatReturns("{}")
                .FromEndpoint("GET", $"conversations/{ConvId}/messages?limit=50&offset=10",
                    Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            client.ListConversationMessages(ConvId, 50, 10);
            restClient.Verify();
        }

        [TestMethod]
        public void PostMessage()
        {
            var restClient = MockRestClient
                .ThatExpects(@"{""type"": ""text"",""content"":{""text"": ""Hello!""}}")
                .AndReturns(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Responses",
                    "ConversationMessage.json")))
                .FromEndpoint("POST", $"conversations/{ConvId}/messages", Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            var req = new MessageSendRequest
            {
                Type = ContentType.Text,
                Content = new Content
                {
                    Text = "Hello!"
                }
            };
            var message = client.SendConversationMessage(ConvId, req);
            restClient.Verify();

            Assert.AreEqual(ConvId, message.ConversationId);
            Assert.AreEqual(MessageStatus.Pending, message.Status);
            Assert.AreEqual(MessageDirection.Sent, message.Direction);
        }

        [TestMethod]
        public void ViewMessage()
        {
            var restClient = MockRestClient
                .ThatReturns(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Responses",
                    "ConversationMessage.json")))
                .FromEndpoint("GET", $"messages/{MsgId}", Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            var message = client.ViewConversationMessage(MsgId);
            restClient.Verify();

            Assert.AreEqual(MsgId, message.Id);
            Assert.AreEqual(ConvId, message.ConversationId);
            Assert.AreEqual(MessageStatus.Pending, message.Status);
            Assert.AreEqual(MessageDirection.Sent, message.Direction);
        }


        [TestMethod]
        public void PostWebhook()
        {
            var restClient = MockRestClient
                .ThatExpects(
                    @"{""events"":[""message.created"", ""message.updated""],""channelId"": ""853eeb5348e541a595da93b48c61a1ae"",""url"":""https://example.com/webhook""}")
                .AndReturns(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Responses",
                    "WebhookView.json")))
                .FromEndpoint("POST", "webhooks", Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            var req = new Webhook
            {
                ChannelId = "853eeb5348e541a595da93b48c61a1ae",
                Url = "https://example.com/webhook",
                Events = new List<WebhookEvent>
                {
                    WebhookEvent.MessageCreated, WebhookEvent.MessageUpdated
                }
            };
            var webhook = client.CreateConversationWebhook(req);
            restClient.Verify();


            Assert.AreEqual(req.ChannelId, webhook.ChannelId);
            Assert.AreEqual(req.Url, webhook.Url);
            Assert.AreEqual(req.Events.Count, webhook.Events.Count);
            Assert.IsTrue(webhook.Events.Contains(WebhookEvent.MessageCreated));
            Assert.IsTrue(webhook.Events.Contains(WebhookEvent.MessageUpdated));
        }

        [TestMethod]
        public void ViewWebhook()
        {
            var restClient = MockRestClient
                .ThatReturns(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Responses",
                    "WebhookView.json")))
                .FromEndpoint("GET", $"webhooks/{WebhookId}", Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            var webhook = client.ViewConversationWebhook(WebhookId);
            restClient.Verify();


            Assert.AreEqual(WebhookId, webhook.Id);
            Assert.IsTrue(webhook.Events.Contains(WebhookEvent.MessageCreated));
            Assert.IsTrue(webhook.Events.Contains(WebhookEvent.MessageUpdated));
        }

        [TestMethod]
        public void DeleteWebhook()
        {
            var restClient = MockRestClient
                .ThatReturns(string.Empty)
                .FromEndpoint("DELETE", $"webhooks/{WebhookId}", Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            client.DeleteConversationWebhook(WebhookId);
            restClient.Verify();
        }

        [TestMethod]
        public void ListWebhooks()
        {
            var restClient = MockRestClient
                .ThatReturns(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Responses",
                    "WebhookList.json")))
                .FromEndpoint("GET", $"webhooks?limit=20&offset=0",
                    Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            var messages = client.ListConversationWebhooks();
            restClient.Verify();

            Assert.AreEqual(10, messages.TotalCount);
            Assert.AreEqual(2, messages.Count);
        }

        [TestMethod]
        public void ListWebhooksPagination()
        {
            var restClient = MockRestClient
                .ThatReturns("{}")
                .FromEndpoint("GET", $"webhooks?limit=50&offset=10",
                    Resource.ConverstationsEndpoint)
                .Get();
            var client = Client.Create(restClient.Object);

            client.ListConversationWebhooks(50, 10);
            restClient.Verify();
        }
    }
}