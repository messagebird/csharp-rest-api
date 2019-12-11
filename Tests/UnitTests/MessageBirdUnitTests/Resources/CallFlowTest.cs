using System.Collections.Generic;
using System.Linq;
using MessageBird;
using MessageBird.Resources.Voice;
using MessageBird.Objects.Voice;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageBirdUnitTests.Resources
{
    [TestClass]
    public class CallFlowTest
    {
        private string baseUrl = VoiceBaseResource<CallFlow>.baseUrl;

        [TestMethod]
        public void Create()
        {
            var restClient = MockRestClient
                .ThatExpects("{  \"title\": \"Forward call to 31612345678\",  \"record\": true,  \"steps\": [    {      \"options\": {        \"destination\": \"31612345678\"      },      \"action\": \"transfer\"    }  ]}")
                .AndReturns("{  \"data\": [    {      \"id\": \"de3ed163-d5fc-45f4-b8c4-7eea7458c635\",      \"title\": \"Forward call to 31612345678\",      \"record\": true,      \"steps\": [        {          \"id\": \"2fa1383e-6f21-4e6f-8c36-0920c3d0730b\",          \"action\": \"transfer\",          \"options\": {            \"destination\": \"31612345678\"          }        }      ],      \"createdAt\": \"2017-03-06T14:52:22Z\",      \"updatedAt\": \"2017-03-06T14:52:22Z\"    }  ],  \"_links\": {    \"self\": \"/call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635\"  }}")
                .FromEndpoint("POST", "call-flows", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var newCallFlow = new CallFlow
            {
                Title = "Forward call to 31612345678",
                Record = true,
                Steps = new List<Step> { new Step { Action = "transfer", Options = new Options { Destination = "31612345678" } } }
            };

            var callFlowResponse = client.CreateCallFlow(newCallFlow);
            restClient.Verify();

            Assert.IsNotNull(callFlowResponse.Data);
            Assert.IsNotNull(callFlowResponse.Links);

            var links = callFlowResponse.Links;
            Assert.AreEqual("/call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635", links.GetValueOrDefault("self"));

            var callFlow = callFlowResponse.Data.FirstOrDefault();
            Assert.AreEqual("de3ed163-d5fc-45f4-b8c4-7eea7458c635", callFlow.Id);
            Assert.AreEqual("Forward call to 31612345678", callFlow.Title);   
            Assert.AreEqual(true, callFlow.Record); 
            Assert.IsNotNull(callFlow.CreatedAt);
            Assert.IsNotNull(callFlow.UpdatedAt);        
            Assert.IsNotNull(callFlow.Steps);

            var step = callFlow.Steps.FirstOrDefault();
            Assert.AreEqual("2fa1383e-6f21-4e6f-8c36-0920c3d0730b", step.Id);
            Assert.AreEqual("transfer", step.Action);
            Assert.AreEqual("31612345678", step.Options.Destination);
        }

        [TestMethod]
        public void Delete()
        {
            var restClient = MockRestClient
                .ThatReturns(string.Empty)
                .FromEndpoint("DELETE", "call-flows/foo-id", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            client.DeleteCallFlow("foo-id");
            restClient.Verify();
        }

        [TestMethod]
        public void List()
        {
            var restClient = MockRestClient
                .ThatReturns(filename: "CallFlowList.json")
                .FromEndpoint("GET", "call-flows?limit=20&offset=0", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var callFlowList = client.ListCallFlows();
            restClient.Verify();

            Assert.IsNotNull(callFlowList.Data);
            Assert.IsNotNull(callFlowList.Links);
            Assert.IsNotNull(callFlowList.Pagination);

            var listLinks = callFlowList.Links;
            Assert.AreEqual("/call-flows?page=1", listLinks.GetValueOrDefault("self"));

            var callFlow = callFlowList.Data.FirstOrDefault();
            Assert.AreEqual("de3ed163-d5fc-45f4-b8c4-7eea7458c635", callFlow.Id);
            Assert.AreEqual("Forward call to 31612345678", callFlow.Title);   
            Assert.AreEqual(false, callFlow.Record); 
            Assert.IsNotNull(callFlow.CreatedAt);
            Assert.IsNotNull(callFlow.UpdatedAt);     
            Assert.IsNotNull(callFlow.Steps);
            Assert.IsNotNull(callFlow.Links);

            var links = callFlow.Links;
            Assert.AreEqual("/call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635", links["self"]);

            var step = callFlow.Steps.FirstOrDefault();
            Assert.AreEqual("3538a6b8-5a2e-4537-8745-f72def6bd393", step.Id);
            Assert.AreEqual("transfer", step.Action);
            Assert.AreEqual("31612345678", step.Options.Destination);

            var pagination = callFlowList.Pagination;
            Assert.AreEqual(1, pagination.TotalCount);
            Assert.AreEqual(1, pagination.PageCount);
            Assert.AreEqual(1, pagination.CurrentPage);
            Assert.AreEqual(10, pagination.PerPage);
        }

        [TestMethod]
        public void Update()
        {
            var restClient = MockRestClient
                .ThatExpects("{\"id\":\"de3ed163-d5fc-45f4-b8c4-7eea7458c635\",\"title\":\"Updated call flow\",\"steps\":[{\"action\":\"transfer\",\"options\":{\"destination\":\"31611223344\"}}]}")
                .AndReturns(filename: "CallFlowUpdate.json")
                .FromEndpoint("PUT", "call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var callFlow = new CallFlow
            {
                Title = "Updated call flow",
                Steps = new List<Step> { new Step { Action = "transfer", Options = new Options { Destination = "31611223344" } } }
            };

            var callFlowResponse = client.UpdateCallFlow("de3ed163-d5fc-45f4-b8c4-7eea7458c635", callFlow);
            restClient.Verify();

            Assert.IsNotNull(callFlowResponse.Data);
            Assert.IsNotNull(callFlowResponse.Links);

            var links = callFlowResponse.Links;
            Assert.AreEqual("/call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635", links.GetValueOrDefault("self"));

            var updatedCallFlow = callFlowResponse.Data.FirstOrDefault();
            Assert.AreEqual("de3ed163-d5fc-45f4-b8c4-7eea7458c635", updatedCallFlow.Id);
            Assert.AreEqual("Updated call flow", updatedCallFlow.Title);   
            Assert.AreEqual(false, updatedCallFlow.Record); 
            Assert.IsNotNull(updatedCallFlow.CreatedAt);
            Assert.IsNotNull(updatedCallFlow.UpdatedAt);        
            Assert.IsNotNull(updatedCallFlow.Steps);

            var step = updatedCallFlow.Steps.FirstOrDefault();
            Assert.AreEqual("3538a6b8-5a2e-4537-8745-f72def6bd393", step.Id);
            Assert.AreEqual("transfer", step.Action);
            Assert.AreEqual("31611223344", step.Options.Destination);           
        }

        [TestMethod]
        public void View()
        {
            var restClient = MockRestClient
                .ThatReturns(filename: "CallFlowView.json")
                .FromEndpoint("GET", "call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var callFlowResponse = client.ViewCallFlow("de3ed163-d5fc-45f4-b8c4-7eea7458c635");
            restClient.Verify();

            Assert.IsNotNull(callFlowResponse.Data);
            Assert.IsNotNull(callFlowResponse.Links);

            var links = callFlowResponse.Links;
            Assert.AreEqual("/call-flows/de3ed163-d5fc-45f4-b8c4-7eea7458c635", links.GetValueOrDefault("self"));

            var callFlow = callFlowResponse.Data.FirstOrDefault();
            Assert.AreEqual("de3ed163-d5fc-45f4-b8c4-7eea7458c635", callFlow.Id);
            Assert.AreEqual("Forward call to 31611223344", callFlow.Title);   
            Assert.AreEqual(false, callFlow.Record); 
            Assert.IsNotNull(callFlow.CreatedAt);
            Assert.IsNotNull(callFlow.UpdatedAt);        
            Assert.IsNotNull(callFlow.Steps);

            var step = callFlow.Steps.FirstOrDefault();
            Assert.AreEqual("3538a6b8-5a2e-4537-8745-f72def6bd393", step.Id);
            Assert.AreEqual("transfer", step.Action);
            Assert.AreEqual("31611223344", step.Options.Destination);
        }
    }
}
