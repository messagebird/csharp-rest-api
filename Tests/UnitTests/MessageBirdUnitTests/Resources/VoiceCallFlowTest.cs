using System.Collections.Generic;
using System.Linq;
using MessageBird;
using MessageBird.Objects.Voice;
using MessageBird.Resources.Voice;
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

            var voiceCallFlowResponse = client.CreateVoiceCallFlow(newVoiceCallFlow);
            restClient.Verify();

            Assert.IsNotNull(voiceCallFlowResponse.Data);
            Assert.IsNotNull(voiceCallFlowResponse.Links);

            var links = voiceCallFlowResponse.Links;
            Assert.AreEqual("/call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635", links.Self);

            var voiceCallFlow = voiceCallFlowResponse.Data.FirstOrDefault();
            Assert.AreEqual("de3ed163-d5fc-45f4-b8c4-7eea7458c635", voiceCallFlow.Id);
            Assert.AreEqual("Forward call to 31612345678", voiceCallFlow.Title);   
            Assert.AreEqual(true, voiceCallFlow.Record); 
            Assert.IsNotNull(voiceCallFlow.CreatedAt);
            Assert.IsNotNull(voiceCallFlow.UpdatedAt);        
            Assert.IsNotNull(voiceCallFlow.Steps);

            var step = voiceCallFlow.Steps.FirstOrDefault();
            Assert.AreEqual("2fa1383e-6f21-4e6f-8c36-0920c3d0730b", step.Id);
            Assert.AreEqual("transfer", step.Action);
            Assert.AreEqual("31612345678", step.Options.Destination);
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

            var voiceCallFlowList = client.ListVoiceCallFlows();
            restClient.Verify();

            Assert.IsNotNull(voiceCallFlowList.Data);
            Assert.IsNotNull(voiceCallFlowList.Links);
            Assert.IsNotNull(voiceCallFlowList.Pagination);

            var listLinks = voiceCallFlowList.Links;
            Assert.AreEqual("/call-flows?page=1", listLinks.Self);

            var voiceCallFlow = voiceCallFlowList.Data.FirstOrDefault();
            Assert.AreEqual("de3ed163-d5fc-45f4-b8c4-7eea7458c635", voiceCallFlow.Id);
            Assert.AreEqual("Forward call to 31612345678", voiceCallFlow.Title);   
            Assert.AreEqual(false, voiceCallFlow.Record); 
            Assert.IsNotNull(voiceCallFlow.CreatedAt);
            Assert.IsNotNull(voiceCallFlow.UpdatedAt);     
            Assert.IsNotNull(voiceCallFlow.Steps);
            Assert.IsNotNull(voiceCallFlow.Links);

            var links = voiceCallFlow.Links;
            Assert.AreEqual("/call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635", links.Self);

            var step = voiceCallFlow.Steps.FirstOrDefault();
            Assert.AreEqual("3538a6b8-5a2e-4537-8745-f72def6bd393", step.Id);
            Assert.AreEqual("transfer", step.Action);
            Assert.AreEqual("31612345678", step.Options.Destination);

            var pagination = voiceCallFlowList.Pagination;
            Assert.AreEqual(1, pagination.TotalCount);
            Assert.AreEqual(1, pagination.PageCount);
            Assert.AreEqual(1, pagination.CurrentPage);
            Assert.AreEqual(10, pagination.PerPage);
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

            var voiceCallFlowResponse = client.UpdateVoiceCallFlow("de3ed163-d5fc-45f4-b8c4-7eea7458c635", voiceCallFlow);
            restClient.Verify();

            Assert.IsNotNull(voiceCallFlowResponse.Data);
            Assert.IsNotNull(voiceCallFlowResponse.Links);

            var links = voiceCallFlowResponse.Links;
            Assert.AreEqual("/call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635", links.Self);

            var updatedVoiceCallFlow = voiceCallFlowResponse.Data.FirstOrDefault();
            Assert.AreEqual("de3ed163-d5fc-45f4-b8c4-7eea7458c635", updatedVoiceCallFlow.Id);
            Assert.AreEqual("Updated call flow", updatedVoiceCallFlow.Title);   
            Assert.AreEqual(false, updatedVoiceCallFlow.Record); 
            Assert.IsNotNull(updatedVoiceCallFlow.CreatedAt);
            Assert.IsNotNull(updatedVoiceCallFlow.UpdatedAt);        
            Assert.IsNotNull(updatedVoiceCallFlow.Steps);

            var step = updatedVoiceCallFlow.Steps.FirstOrDefault();
            Assert.AreEqual("3538a6b8-5a2e-4537-8745-f72def6bd393", step.Id);
            Assert.AreEqual("transfer", step.Action);
            Assert.AreEqual("31611223344", step.Options.Destination);           
        }

        [TestMethod]
        public void View()
        {
            var restClient = MockRestClient
                .ThatReturns(filename: "VoiceCallFlowView.json")
                .FromEndpoint("GET", "call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635", VoiceCallFlowsResource.VoiceCallFlowsBaseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var voiceCallFlowResponse = client.ViewVoiceCallFlow("de3ed163-d5fc-45f4-b8c4-7eea7458c635");
            restClient.Verify();

            Assert.IsNotNull(voiceCallFlowResponse.Data);
            Assert.IsNotNull(voiceCallFlowResponse.Links);

            var links = voiceCallFlowResponse.Links;
            Assert.AreEqual("/call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635", links.Self);

            var voiceCallFlow = voiceCallFlowResponse.Data.FirstOrDefault();
            Assert.AreEqual("de3ed163-d5fc-45f4-b8c4-7eea7458c635", voiceCallFlow.Id);
            Assert.AreEqual("Forward call to 31611223344", voiceCallFlow.Title);   
            Assert.AreEqual(false, voiceCallFlow.Record); 
            Assert.IsNotNull(voiceCallFlow.CreatedAt);
            Assert.IsNotNull(voiceCallFlow.UpdatedAt);        
            Assert.IsNotNull(voiceCallFlow.Steps);

            var step = voiceCallFlow.Steps.FirstOrDefault();
            Assert.AreEqual("3538a6b8-5a2e-4537-8745-f72def6bd393", step.Id);
            Assert.AreEqual("transfer", step.Action);
            Assert.AreEqual("31611223344", step.Options.Destination);
        }
    }
}
