using MessageBird;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using MessageBird.Objects.Voice;
using MessageBird.Resources.Voice;
using MessageBird.Exceptions;


namespace MessageBirdUnitTests.Resources
{
    [TestClass]

    public class CallTest
    {
        [TestMethod]
        public void Create()
        {
            var restClient = MockRestClient
                .ThatExpects("{\"source\":\"31644556677\",\"destination\":\"33766723144\",\"callFlow\":{\"title\":\"Forward call to 31612345678\",\"record\":true,\"steps\":[{\"action\":\"transfer\",\"options\":{\"destination\":\"31612345678\"}}]},\"duration\":0}")
                .AndReturns("{\"data\":[{\"id\":\"cac63a43-ff05-4cc3-b8e3-fca82e97975c\",\"source\":\"31644556677\",\"destination\":\"33766723144\",\"createdAt\":\"2019-12-06T15:56:39Z\",\"updatedAt\":\"2019-12-06T15:56:39Z\",\"endedAt\":null}],\"_links\":{\"self\":\"/calls/cac63a43-ff05-4cc3-b8e3-fca82e97975c\"},\"pagination\":{\"totalCount\":0,\"pageCount\":0,\"currentPage\":0,\"perPage\":0}}\n")
                .FromEndpoint("POST", "calls", CallsResource.CallBaseUrl)
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
                source = "31644556677",
                destination = "33766723144",
                callFlow = newCallFlow
            };
            var callResponse = client.CreateCall(newCall);
            restClient.Verify();

            Assert.IsNotNull(callResponse.Data);

            var call = callResponse.Data.FirstOrDefault();
            Assert.AreEqual("cac63a43-ff05-4cc3-b8e3-fca82e97975c", call.Id);
        }

    }
}
