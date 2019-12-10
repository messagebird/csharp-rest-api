using MessageBird;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using MessageBird.Objects.Voice;
using MessageBird.Resources.Voice;


namespace MessageBirdUnitTests.Resources
{
    [TestClass]
    public class CallTest
    {
        private string baseUrl = VoiceBaseResource<Call>.baseUrl;
        [TestMethod]
        public void Create()
        {
            var restClient = MockRestClient
                .ThatExpects("{\"source\":\"31644556677\",\"destination\":\"33766723144\",\"callFlow\":{\"title\":\"Forward call to 31612345678\",\"record\":true,\"steps\":[{\"action\":\"transfer\",\"options\":{\"destination\":\"31612345678\"}}]},\"duration\":0}")
                .AndReturns(filename: "CallCreate.json")
                .FromEndpoint("POST", "calls", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var newCallFlow = new CallFlow
            {
                Title = "Forward call to 31612345678",
                Record = true,
                Steps = new List<Step> { new Step { Action = "transfer", Options = new Options { Destination = "31612345678" } } }
            };
            var newCall = new Call
            {   
                Source = "31644556677",
                Destination = "33766723144",
                CallFlow = newCallFlow
            };
            var callResponse = client.CreateCall(newCall);
            restClient.Verify();

            Assert.IsNotNull(callResponse.Data);

            var call = callResponse.Data.FirstOrDefault();
            Assert.AreEqual("cac63a43-ff05-4cc3-b8e3-fca82e97975c", call.Id);
        }

        [TestMethod]
        public void List()
        {
            var restClient = MockRestClient
                .ThatReturns(filename: "CallList.json")
                .FromEndpoint("GET", "calls?limit=20&offset=0", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var callList = client.ListCalls();
            restClient.Verify();

            Assert.IsNotNull(callList.Data);
            Assert.IsNotNull(callList.Links);
            Assert.IsNotNull(callList.Pagination);

            var selfLink = callList.Links["self"];
            Assert.AreEqual("/calls?page=1", selfLink);

            var call = callList.Data.FirstOrDefault();
            Assert.AreEqual("f1aa71c0-8f2a-4fe8-b5ef-9a330454ef58", call.Id);
            Assert.AreEqual(CallStatus.Ended, call.Status);
            Assert.AreEqual("31644556677", call.Source);
            Assert.AreEqual("31612345678", call.Destination);
            Assert.IsNotNull(call.CreatedAt);
            Assert.IsNotNull(call.UpdatedAt);
            Assert.IsNotNull(call.EndedAt);
            Assert.IsNotNull(call.Links);

            var callSelfLink = call.Links["self"];
            Assert.AreEqual("/calls/f1aa71c0-8f2a-4fe8-b5ef-9a330454ef58", callSelfLink);

            var pagination = callList.Pagination;
            Assert.AreEqual(2, pagination.TotalCount);
            Assert.AreEqual(1, pagination.PageCount);
            Assert.AreEqual(1, pagination.CurrentPage);
            Assert.AreEqual(10, pagination.PerPage);
        }

        [TestMethod]
        public void View()
        {
            var restClient = MockRestClient
                .ThatReturns(filename: "CallView.json")
                .FromEndpoint("GET", "calls/f1aa71c0-8f2a-4fe8-b5ef-9a330454ef58", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var callResponse = client.ViewCall("f1aa71c0-8f2a-4fe8-b5ef-9a330454ef58");
            restClient.Verify();

            Assert.IsNotNull(callResponse.Data);
            Assert.IsNotNull(callResponse.Links);

            var selfLink = callResponse.Links["self"];
            Assert.AreEqual("/calls/f1aa71c0-8f2a-4fe8-b5ef-9a330454ef58", selfLink);

            var call = callResponse.Data.FirstOrDefault();
            Assert.AreEqual("f1aa71c0-8f2a-4fe8-b5ef-9a330454ef58", call.Id);
            Assert.AreEqual(CallStatus.Ended, call.Status);
            Assert.AreEqual("31644556677", call.Source);
            Assert.AreEqual("31612345678", call.Destination);
            Assert.IsNotNull(call.CreatedAt);
            Assert.IsNotNull(call.UpdatedAt);
            Assert.IsNotNull(call.EndedAt);     
            Assert.IsNull(call.Links);
        }

        [TestMethod]
        public void Delete()
        {
            var restClient = MockRestClient
                .ThatReturns(string.Empty)
                .FromEndpoint("DELETE", "calls/f1aa71c0-8f2a-4fe8-b5ef-9a330454ef58", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            client.DeleteCall("f1aa71c0-8f2a-4fe8-b5ef-9a330454ef58");
            restClient.Verify();
        }
    }
}
