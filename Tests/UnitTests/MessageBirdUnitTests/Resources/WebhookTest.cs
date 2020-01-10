using MessageBird;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using MessageBird.Objects.Voice;
using MessageBird.Resources.Voice;

namespace MessageBirdUnitTests.Resources
{
    [TestClass]
    public class WebhookTest
    {
        private string baseUrl = VoiceBaseResource<Webhook>.baseUrl;
        [TestMethod]
        public void Create()
        {
            var restClient = MockRestClient
                .ThatExpects("{\"url\":\"https://testing.com\",\"token\":\"example token\"}")
                .AndReturns(filename: "WebhooksCreate.json")
                .FromEndpoint("POST", "webhooks", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var newWebhook = new Webhook
            {
                url = "https://testing.com",
                token = "example token"
            };

            var webhookResponse = client.CreateWebhook(newWebhook);
            restClient.Verify();

            Assert.IsNotNull(webhookResponse.Data);
            Assert.IsNotNull(webhookResponse.Links);

            var selfLink = webhookResponse.Links["self"];
            Assert.AreEqual("/webhooks/dff95aec-c11f-423c-82f3-1f391d78d716", selfLink);

            var webhook = webhookResponse.Data.FirstOrDefault();
            Assert.AreEqual("dff95aec-c11f-423c-82f3-1f391d78d716", webhook.Id);
            Assert.AreEqual("https://testing.com", webhook.url);
            Assert.AreEqual("example token", webhook.token);
            Assert.IsNotNull(webhook.createdAt);
            Assert.IsNotNull(webhook.updatedAt);
        }

        [TestMethod]
        public void List()
        {
            var restClient = MockRestClient
                .ThatReturns(filename: "WebhooksList.json")
                .FromEndpoint("GET", "webhooks?limit=20&offset=0", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);
            var webhookList = client.ListWebhooks();
            restClient.Verify();

            Assert.IsNotNull(webhookList.Data);
            Assert.IsNotNull(webhookList.Links);
            Assert.IsNotNull(webhookList.Pagination);

            var selfLink = webhookList.Links["self"];
            Assert.AreEqual("/webhooks?page=1", selfLink);

            var webhook = webhookList.Data.FirstOrDefault();
            Assert.AreEqual("25fde85f-5e8f-42ef-9709-92241ae3789d", webhook.Id);
            Assert.AreEqual("https://newurl.com", webhook.url);
            Assert.AreEqual("test title", webhook.token);
            Assert.IsNotNull(webhook.createdAt);
            Assert.IsNotNull(webhook.updatedAt);

            var pagination = webhookList.Pagination;
            Assert.AreEqual(2, pagination.TotalCount);
            Assert.AreEqual(1, pagination.PageCount);
            Assert.AreEqual(1, pagination.CurrentPage);
            Assert.AreEqual(10, pagination.PerPage);
        }

        [TestMethod]
        public void View()
        {
            var restClient = MockRestClient
                .ThatReturns(filename: "WebhooksView.json")
                .FromEndpoint("GET", "webhooks/dff95aec-c11f-423c-82f3-1f391d78d716", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);
            var webhookResponse = client.ViewWebhook("dff95aec-c11f-423c-82f3-1f391d78d716");
            restClient.Verify();

            Assert.IsNotNull(webhookResponse.Data);
            Assert.IsNotNull(webhookResponse.Links);

            var selfLink = webhookResponse.Links["self"];
            Assert.AreEqual("/webhooks/dff95aec-c11f-423c-82f3-1f391d78d716", selfLink);

            var webhook = webhookResponse.Data.FirstOrDefault();
            Assert.AreEqual("dff95aec-c11f-423c-82f3-1f391d78d716", webhook.Id);
            Assert.AreEqual("https://testing.com", webhook.url);
            Assert.AreEqual("example token", webhook.token);
            Assert.IsNotNull(webhook.createdAt);
            Assert.IsNotNull(webhook.updatedAt);
        }

        [TestMethod]
        public void Update()
        {
            var restClient = MockRestClient
                .ThatExpects("{\"url\":\"https://example.com/update\",\"token\":\"new token\"}")
                .AndReturns(filename: "WebhooksUpdate.json")
                .FromEndpoint("PUT", "webhooks/dff95aec-c11f-423c-82f3-1f391d78d716", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var updateWebhook = new Webhook
            {
                url = "https://example.com/update",
                token = "new token"
            };

            var webhookResponse = client.UpdateWebhook("dff95aec-c11f-423c-82f3-1f391d78d716", updateWebhook);
            restClient.Verify();

            Assert.IsNotNull(webhookResponse.Data);
            Assert.IsNotNull(webhookResponse.Links);

            var selfLink = webhookResponse.Links["self"];
            Assert.AreEqual("/webhooks/dff95aec-c11f-423c-82f3-1f391d78d716", selfLink);

            var webhook = webhookResponse.Data.FirstOrDefault();
            Assert.AreEqual("dff95aec-c11f-423c-82f3-1f391d78d716", webhook.Id);
            Assert.AreEqual("https://example.com/update", webhook.url);
            Assert.AreEqual("new token", webhook.token);
            Assert.IsNotNull(webhook.createdAt);
            Assert.IsNotNull(webhook.updatedAt);
        }

        [TestMethod]
        public void Delete()
        {
            var restClient = MockRestClient
                .ThatReturns(string.Empty)
                .FromEndpoint("DELETE", "webhooks/f1aa71c0-8f2a-4fe8-b5ef-9a330454ef58", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            client.DeleteWebhook("f1aa71c0-8f2a-4fe8-b5ef-9a330454ef58");
            restClient.Verify();
        }

    }
}
