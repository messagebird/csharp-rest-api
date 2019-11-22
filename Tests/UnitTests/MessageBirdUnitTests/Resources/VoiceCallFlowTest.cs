using System.Collections.Generic;
using MessageBird;
using MessageBird.Objects.VoiceCalls;
using MessageBird.Resources.VoiceCalls;
using MessageBirdTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageBirdUnitTests.Resources
{
    [TestClass]
    public class VoiceCallFlowTest
    {
        [TestMethod]
        public void Create()
        {
            var restClient = MockRestClient
                .ThatExpects("{  \"title\": \"Forward call to 31612345678\",  \"record\": true,  \"steps\": [    {      \"options\": {        \"destination\": \"31612345678\"      },      \"action\": \"transfer\"    }  ]}")
                .AndReturns("{  \"data\": [    {      \"id\": \"de3ed163-d5fc-45f4-b8c4-7eea7458c635\",      \"title\": \"Forward call to 31612345678\",      \"record\": true,      \"steps\": [        {          \"id\": \"2fa1383e-6f21-4e6f-8c36-0920c3d0730b\",          \"action\": \"transfer\",          \"options\": {            \"destination\": \"31612345678\"          }        }      ],      \"createdAt\": \"2017-03-06T14:52:22Z\",      \"updatedAt\": \"2017-03-06T14:52:22Z\"    }  ],  \"_links\": {    \"self\": \"/call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635\"  }}")
                .FromEndpoint("POST", "call-flows", VoiceCallFlowsResource.VoiceCallFlowsBaseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var newVoiceCallFlow = new VoiceCallFlow
            {
                Title = "Forward call to 31612345678",
                Record = true,
                Steps = new List<Step> { new Step { Action = "transfer", Options = new Options { Destination = "31612345678" } } }
            };
            var voiceCallFlow = client.CreateVoiceCallFlow(newVoiceCallFlow);
            restClient.Verify();

            Assert.AreEqual("Forward call to 31612345678", voiceCallFlow.Title);
        }

        [TestMethod]
        public void Delete()
        {
            var restClient = MockRestClient
                .ThatReturns(string.Empty)
                .FromEndpoint("DELETE", "call-flows/foo-id", VoiceCallFlowsResource.VoiceCallFlowsBaseUrl)
                .Get();
            var client = Client.Create(restClient.Object);

            client.DeleteVoiceCallFlow("foo-id");
            restClient.Verify();
        }

        [TestMethod]
        public void List()
        {
            var restClient = MockRestClient
                .ThatReturns(filename: "VoiceCallFlowList.json")
                .FromEndpoint("GET", "call-flows?limit=20&offset=0", VoiceCallFlowsResource.VoiceCallFlowsBaseUrl)
                .Get();


            var client = Client.Create(restClient.Object);

            var voiceCallFlows = client.ListVoiceCallFlows();
            restClient.Verify();


            Assert.AreEqual(1, voiceCallFlows.Pagination.TotalCount);
        }

        [TestMethod]
        public void Update()
        {
            var restClient = MockRestClient
                .ThatExpects("{\"title\":\"Updated call flow\",\"steps\":[{\"options\":{\"destination\":\"31611223344\"},\"action\":\"transfer\"}]}")
                .AndReturns(filename: "VoiceCallFlowUpdate.json")
                .FromEndpoint("PUT", "call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635", VoiceCallFlowsResource.VoiceCallFlowsBaseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var voiceCallFlow = new VoiceCallFlow
            {
                Title = "Updated call flow",
                Steps = new List<Step> { new Step { Action = "transfer", Options = new Options { Destination = "31611223344" } } }
            };
            var updatedVoiceCallFlow = client.UpdateVoiceCallFlow("de3ed163-d5fc-45f4-b8c4-7eea7458c635", voiceCallFlow);
            restClient.Verify();

            Assert.IsNotNull(updatedVoiceCallFlow.Id);
        }

        [TestMethod]
        public void View()
        {
            var restClient = MockRestClient
                .ThatReturns(filename: "VoiceCallFlowView.json")
                .FromEndpoint("GET", "call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635", VoiceCallFlowsResource.VoiceCallFlowsBaseUrl)
                .Get();
            var client = Client.Create(restClient.Object);

            var voiceCallFlow = client.ViewVoiceCallFlow("de3ed163-d5fc-45f4-b8c4-7eea7458c635");
            restClient.Verify();

            Assert.AreEqual("de3ed163-d5fc-45f4-b8c4-7eea7458c635", voiceCallFlow.Id);
        }
    }
}
