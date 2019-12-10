using System.IO;
using System.Linq;
using MessageBird;
using MessageBird.Resources.Voice;
using MessageBird.Objects.Voice;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageBirdUnitTests.Resources
{
    [TestClass]
    public class RecordingTest
    {
        private string baseUrl = VoiceBaseResource<Recording>.baseUrl;  
        [TestMethod]
        public void List()
        {
            var restClient = MockRestClient
                .ThatReturns(filename: "RecordingList.json")
                .FromEndpoint("GET", "calls/fdcf0391-4fdc-4e38-9551-e8a01602984f/legs/317bd14d-3eee-4380-b01f-fe7723c6913a/recordings?limit=5&offset=2", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);
            var callId = "fdcf0391-4fdc-4e38-9551-e8a01602984f";
            var legId = "317bd14d-3eee-4380-b01f-fe7723c6913a";
            var recordingList = client.ListRecordings(callId: callId, legId: legId, limit: 5, offset: 2);
            restClient.Verify();

            Assert.IsNotNull(recordingList.Data);
            Assert.IsNotNull(recordingList.Links);
            Assert.IsNotNull(recordingList.Pagination);

            var selfLink = recordingList.Links["self"];
            Assert.AreEqual("/calls/fdcf0391-4fdc-4e38-9551-e8a01602984f/legs/317bd14d-3eee-4380-b01f-fe7723c6913a/recordings/4c2ac358-b467-4f7a-a6c8-6157ad181142?page=1", selfLink);

            var recording = recordingList.Data.FirstOrDefault();
            Assert.AreEqual("4c2ac358-b467-4f7a-a6c8-6157ad181142", recording.Id);
            Assert.AreEqual("317bd14d-3eee-4380-b01f-fe7723c6913a", recording.LegId);
            Assert.AreEqual("wav", recording.Format);
            Assert.AreEqual("ivr", recording.Type);
            Assert.AreEqual("done", recording.Status);
            Assert.AreEqual(42, recording.Duration);
            Assert.IsNotNull(recording.CreatedAt);
            Assert.IsNotNull(recording.UpdatedAt);
            Assert.IsNotNull(recording.Links);

            var recordingSelfLink = recording.Links["self"];
            Assert.AreEqual("/calls/fdcf0391-4fdc-4e38-9551-e8a01602984f/legs/317bd14d-3eee-4380-b01f-fe7723c6913a/recordings/4c2ac358-b467-4f7a-a6c8-6157ad181142", recordingSelfLink);

            var recordingFileLink = recording.Links["file"];
            Assert.AreEqual("/calls/fdcf0391-4fdc-4e38-9551-e8a01602984f/legs/317bd14d-3eee-4380-b01f-fe7723c6913a/recordings/4c2ac358-b467-4f7a-a6c8-6157ad181142.wav", recordingFileLink);

            var pagination = recordingList.Pagination;
            Assert.AreEqual(1, pagination.TotalCount);
            Assert.AreEqual(1, pagination.PageCount);
            Assert.AreEqual(1, pagination.CurrentPage);
            Assert.AreEqual(10, pagination.PerPage);
        }

        [TestMethod]
        public void View()
        {
            var restClient = MockRestClient
                .ThatReturns(filename: "RecordingView.json")
                .FromEndpoint("GET", "calls/bb3f0391-4fdc-4e38-9551-e8a01602984f/legs/cc3bd14d-3eee-4380-b01f-fe7723c69a31/recordings/3b4ac358-9467-4f7a-a6c8-6157ad181123", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var recordingResponse = client.ViewRecording("bb3f0391-4fdc-4e38-9551-e8a01602984f", "cc3bd14d-3eee-4380-b01f-fe7723c69a31", "3b4ac358-9467-4f7a-a6c8-6157ad181123");
            restClient.Verify();

            Assert.IsNotNull(recordingResponse.Data);
            Assert.IsNotNull(recordingResponse.Links);

            var selfLink = recordingResponse.Links["self"];
            Assert.AreEqual("/calls/bb3f0391-4fdc-4e38-9551-e8a01602984f/legs/cc3bd14d-3eee-4380-b01f-fe7723c69a31/recordings/3b4ac358-9467-4f7a-a6c8-6157ad181123", selfLink);

            var fileLink = recordingResponse.Links["file"];
            Assert.AreEqual("/calls/bb3f0391-4fdc-4e38-9551-e8a01602984f/legs/cc3bd14d-3eee-4380-b01f-fe7723c69a31/recordings/3b4ac358-9467-4f7a-a6c8-6157ad181123.wav", fileLink);

            var recording = recordingResponse.Data.FirstOrDefault();
            Assert.AreEqual("3b4ac358-9467-4f7a-a6c8-6157ad181123", recording.Id);
            Assert.AreEqual("cc3bd14d-3eee-4380-b01f-fe7723c69a31", recording.LegId);
            Assert.AreEqual("wav", recording.Format); 
            Assert.AreEqual("done", recording.Status);
            Assert.AreEqual(7, recording.Duration);
            Assert.IsNotNull(recording.CreatedAt);
            Assert.IsNotNull(recording.UpdatedAt);        
            Assert.IsNull(recording.Links);
        }

        [TestMethod]
        public void Delete()
        {
            var restClient = MockRestClient
                .ThatReturns(string.Empty)
                .FromEndpoint("DELETE", "calls/bb3f0391-4fdc-4e38-9551-e8a01602984f/legs/cc3bd14d-3eee-4380-b01f-fe7723c69a31/recordings/3b4ac358-9467-4f7a-a6c8-6157ad181123", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            client.DeleteRecording("bb3f0391-4fdc-4e38-9551-e8a01602984f", "cc3bd14d-3eee-4380-b01f-fe7723c69a31", "3b4ac358-9467-4f7a-a6c8-6157ad181123");
            restClient.Verify();
        }

        [TestMethod]
        public void Download()
        {
            var restClient = MockRestClient
                .ThatReturns(stream: new MemoryStream())
                .FromEndpoint("GET", "calls/bb3f0391-4fdc-4e38-9551-e8a01602984f/legs/cc3bd14d-3eee-4380-b01f-fe7723c69a31/recordings/3b4ac358-9467-4f7a-a6c8-6157ad181123.wav", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            client.DownloadRecording("bb3f0391-4fdc-4e38-9551-e8a01602984f", "cc3bd14d-3eee-4380-b01f-fe7723c69a31", "3b4ac358-9467-4f7a-a6c8-6157ad181123");
            restClient.Verify();
        }
    }
}
