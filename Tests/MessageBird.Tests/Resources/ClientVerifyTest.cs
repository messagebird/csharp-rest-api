using MessageBird.Net;
using MessageBird.Objects;
using MessageBird.Objects.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WireMock.Server;

namespace MessageBird.Tests.Resources
{
    /**
     * Using this class to be able to change the base url of the request.
     */
    internal class VerifyWithUrl : MessageBird.Resources.Resource
    {
        public VerifyWithUrl(Verify verify)
            : base("verify", verify)
        {
        }

        private string VerifyBaseUrl;

        private string Token
        {
            get
            {
                return (Object != null) ? ((Verify)Object).Token : null;
            }
        }

        public override string QueryString
        {
            get
            {
                return string.IsNullOrEmpty(Token) ? string.Empty : "token=" + System.Uri.EscapeDataString(Token);
            }
        }

        public void SetBaseUrl(string baseUrl)
        {
            VerifyBaseUrl = baseUrl;
        }

        public override string BaseUrl => VerifyBaseUrl;
    }

    [TestClass]
    public class RestClientVerifyTest
    {
        private const string ENDPOINT = "http://localhost:";
        private const string ACCESS_KEY = "SOME_ACCESS_KEY";
        private const long RECIPIENT = 31612345678;
        private const string TEMPLATE = "test template";
        private const string ORIGINATOR = "test ori";
        private const DataEncoding DATA_ENCODING = DataEncoding.Auto;
        private const string REFERENCE = "testref";
        private const int TIMEOUT = 1234;
        private const int TOKEN_LENGTH = 15;
        private const Voice VOICE = Voice.Female;
        private const Language LANGUAGE = Language.BrazilianPortuguese;

        private const string CREATE_VERIFY_RESPONSE = @"{""id"": ""2cec4f419b7a4cf7b546a41c630a20b0"",""href"": ""some_ur"",""recipient"": 31627343907,""reference"": null,""messages"": {""href"": ""some_url""},""status"": ""sent"",""createdDatetime"": ""2019-07-04T08:23:37+00:00"",""validUntilDatetime"": ""2019-07-04T08:24:07+00:00""}";
        private const string EXPECTED_REQUEST = @"{""recipient"":31612345678,""reference"":""testref"",""originator"":""test ori"",""template"":""test template"",""datacoding"":""auto"",""tokenLength"":15,""type"":""tts"",""timeout"":1234,""voice"":""female"",""language"":""pt-br""}";

        [TestMethod]
        public void CreateVerifyTest()
        {
            // arrange
            var server = FluentMockServer.Start();
            int port = server.Ports[0];


            var restClient = new RestClient(ENDPOINT + port, ACCESS_KEY, null);

            var verifyOptionalArguments = new VerifyOptionalArguments()
            {
                Template = TEMPLATE,
                Encoding = DATA_ENCODING,
                Originator = ORIGINATOR,
                Reference = REFERENCE,
                Type = MessageType.Tts,
                Timeout = TIMEOUT,
                TokenLength = TOKEN_LENGTH,
                Voice = VOICE,
                Language = LANGUAGE
            };

            server
                      .Given(
                        WireMock.RequestBuilders.Request.Create().WithPath("/verify").UsingPost()
                      )
                      .RespondWith(
                        WireMock.ResponseBuilders.Response.Create()
                          .WithStatusCode(201)
                          .WithHeader("Content-Type", "application/json")
                          .WithBody(CREATE_VERIFY_RESPONSE)
                      );

            // act
            var verify = new Verify(RECIPIENT, verifyOptionalArguments);
            var verifyResource = new VerifyWithUrl(verify);
            verifyResource.SetBaseUrl(ENDPOINT + port);

            restClient.Create(verifyResource);

            // assert
            var allRequests = server.LogEntries;
            var allRequestsEnumerator = allRequests.GetEnumerator();
            allRequestsEnumerator.MoveNext();
            var logEntry = allRequestsEnumerator.Current;

            Assert.IsNotNull(logEntry);
            Assert.AreEqual(EXPECTED_REQUEST, logEntry.RequestMessage.Body);
            allRequestsEnumerator.MoveNext();
            Assert.IsNull(allRequestsEnumerator.Current);

            server.Dispose();
            allRequestsEnumerator.Dispose();
        }
    }
}
