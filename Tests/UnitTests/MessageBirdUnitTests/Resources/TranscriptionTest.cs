using System.IO;
using System.Linq;
using MessageBird;
using MessageBird.Resources.Voice;
using MessageBird.Objects.Voice;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageBirdUnitTests.Resources
{
    [TestClass]
    public class TranscriptionTest
    {
        private readonly string baseUrl = VoiceBaseResource<Transcription>.baseUrl;

        [TestMethod]
        public void Create()
        {
            var restClient = MockRestClient
                .ThatExpects("{\"Language\":\"en-EN\"}")
                .AndReturns(filename: "TranscriptionCreate.json")
                .FromEndpoint("POST", "calls/373395cc-382b-4a33-b372-cc31f0fdf242/legs/8dd347a4-11ee-44f2-bee3-7fbda300b2cd/recordings/cfa9ae96-e034-4db7-91cb-e58a8392c7bd/transcriptions/", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);
            var transcriptionResponse = client.CreateTranscription("373395cc-382b-4a33-b372-cc31f0fdf242", "8dd347a4-11ee-44f2-bee3-7fbda300b2cd", "cfa9ae96-e034-4db7-91cb-e58a8392c7bd", "en-EN");
            restClient.Verify();

            Assert.IsNotNull(transcriptionResponse.Data);

            var transcription = transcriptionResponse.Data.FirstOrDefault();
            Assert.AreEqual("2ce04c83-ca4f-4d94-8310-02968da41318", transcription.Id);
            Assert.AreEqual("cfa9ae96-e034-4db7-91cb-e58a8392c7bd", transcription.RecordingId);
            Assert.AreEqual("transcribing", transcription.Status);
        }

        [TestMethod]
        public void List()
        {
            var restClient = MockRestClient
                .ThatReturns(filename: "TranscriptionList.json")
                .FromEndpoint("GET", "calls/373395cc-382b-4a33-b372-cc31f0fdf242/legs/8dd347a4-11ee-44f2-bee3-7fbda300b2cd/recordings/cfa9ae96-e034-4db7-91cb-e58a8392c7bd/transcriptions?limit=5&page=1", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);
            var transcriptionList = client.ListTranscriptions("373395cc-382b-4a33-b372-cc31f0fdf242", "8dd347a4-11ee-44f2-bee3-7fbda300b2cd", "cfa9ae96-e034-4db7-91cb-e58a8392c7bd", 5, 1);
            restClient.Verify();

            Assert.IsNotNull(transcriptionList.Data);
            Assert.IsNotNull(transcriptionList.Links);
            Assert.IsNotNull(transcriptionList.Pagination);

            var selfLink = transcriptionList.Links["self"];
            Assert.AreEqual("/calls/373395cc-382b-4a33-b372-cc31f0fdf242/legs/8dd347a4-11ee-44f2-bee3-7fbda300b2cd/recordings/cfa9ae96-e034-4db7-91cb-e58a8392c7bd/transcriptions", selfLink);

            var transcription = transcriptionList.Data.FirstOrDefault();
            Assert.AreEqual("1ce04c83-ca4f-4d94-8310-02968da41317", transcription.Id);
            Assert.AreEqual("cfa9ae96-e034-4db7-91cb-e58a8392c7bd", transcription.RecordingId);
            Assert.AreEqual("done", transcription.Status);
            Assert.IsNotNull(transcription.CreatedAt);
            Assert.IsNotNull(transcription.UpdatedAt);
            Assert.IsNotNull(transcription.Links);

            var transcriptionSelfLink = transcription.Links["self"];
            Assert.AreEqual("/transcriptions/1ce04c83-ca4f-4d94-8310-02968da41317", transcriptionSelfLink);

            var transcriptionFileLink = transcription.Links["file"];
            Assert.AreEqual("/transcriptions/1ce04c83-ca4f-4d94-8310-02968da41317.txt", transcriptionFileLink);

            var pagination = transcriptionList.Pagination;
            Assert.AreEqual(3, pagination.TotalCount);
            Assert.AreEqual(1, pagination.PageCount);
            Assert.AreEqual(1, pagination.CurrentPage);
            Assert.AreEqual(10, pagination.PerPage);
        }

        [TestMethod]
        public void View()
        {
            var restClient = MockRestClient
                .ThatReturns(filename: "TranscriptionView.json")
                .FromEndpoint("GET", "calls/373395cc-382b-4a33-b372-cc31f0fdf242/legs/8dd347a4-11ee-44f2-bee3-7fbda300b2cd/recordings/cfa9ae96-e034-4db7-91cb-e58a8392c7bd/transcriptions/2ce04c83-ca4f-4d94-8310-02968da41318", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            var transcriptionResponse = client.ViewTranscription("373395cc-382b-4a33-b372-cc31f0fdf242", "8dd347a4-11ee-44f2-bee3-7fbda300b2cd", "cfa9ae96-e034-4db7-91cb-e58a8392c7bd", "2ce04c83-ca4f-4d94-8310-02968da41318");
            restClient.Verify();

            Assert.IsNotNull(transcriptionResponse.Data);
            Assert.IsNotNull(transcriptionResponse.Links);

            var selfLink = transcriptionResponse.Links["self"];
            Assert.AreEqual("/calls/373395cc-382b-4a33-b372-cc31f0fdf242/legs/8dd347a4-11ee-44f2-bee3-7fbda300b2cd/recordings/cfa9ae96-e034-4db7-91cb-e58a8392c7bd/transcriptions/2ce04c83-ca4f-4d94-8310-02968da41318", selfLink);

            var fileLink = transcriptionResponse.Links["file"];
            Assert.AreEqual("/calls/373395cc-382b-4a33-b372-cc31f0fdf242/legs/8dd347a4-11ee-44f2-bee3-7fbda300b2cd/recordings/cfa9ae96-e034-4db7-91cb-e58a8392c7bd/transcriptions/2ce04c83-ca4f-4d94-8310-02968da41318.txt", fileLink);

            var transcription = transcriptionResponse.Data.FirstOrDefault();
            Assert.AreEqual("2ce04c83-ca4f-4d94-8310-02968da41318", transcription.Id);
            Assert.AreEqual("cfa9ae96-e034-4db7-91cb-e58a8392c7bd", transcription.RecordingId);
            Assert.AreEqual("done", transcription.Status);
            Assert.IsNotNull(transcription.CreatedAt);
            Assert.IsNotNull(transcription.UpdatedAt);        
            Assert.IsNotNull(transcription.Links);
        }

        [TestMethod]
        public void Download()
        {
            var restClient = MockRestClient
                .ThatReturns(stream: new MemoryStream())
                .FromEndpoint("GET", "calls/373395cc-382b-4a33-b372-cc31f0fdf242/legs/8dd347a4-11ee-44f2-bee3-7fbda300b2cd/recordings/cfa9ae96-e034-4db7-91cb-e58a8392c7bd/transcriptions/2ce04c83-ca4f-4d94-8310-02968da41318.txt", baseUrl)
                .Get();

            var client = Client.Create(restClient.Object);

            client.DownloadTranscription("373395cc-382b-4a33-b372-cc31f0fdf242", "8dd347a4-11ee-44f2-bee3-7fbda300b2cd", "cfa9ae96-e034-4db7-91cb-e58a8392c7bd", "2ce04c83-ca4f-4d94-8310-02968da41318");
            restClient.Verify();
        }
    }
}
