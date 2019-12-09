using System;
using System.IO;
using MessageBird.Net;
using MessageBird.Net.ProxyConfigurationInjector;
using MessageBird.Resources;
using Moq;
using Newtonsoft.Json.Linq;

namespace MessageBirdUnitTests
{
    /// <summary>
    /// Helper that offers a convenient way to create and configure a RestClient mock with a fluent API.
    /// 
    /// <example>
    /// The example below fails the test if the MessageBird Client does not
    /// generate the expected request. Assertions can be made on the response
    /// to assert it is deserialized correctly.
    /// <code>
    /// var restClient = MockRestClient.ThatExpects(requestBody).AndReturns(responseBody).FromEndpoint("POST", "messages").Get();
    /// var client = new Client(restClient);
    /// 
    /// client.SendMessage(...)
    /// </code>
    /// </example>
    /// </summary>
    public class MockRestClient
    {
        private const string AccessKey = "";
        private const IProxyConfigurationInjector ProxyConfigurationInjector = null;

        private MockRepository mockRepository;

        private string RequestBody { get; set; }
        private string ResponseBody { get; set; }
        private Stream ResponseStream { get; set; }
        private string Method { get; set; }
        private string Uri { get; set; }
        private string BaseUrl { get; set; }

        private MockRestClient()
        {
            // MockBehavior.Strict configures the mocks to throw exceptions
            // when invoked without a corresponding setup, rather than
            // returning "default" values (empty arrays, null et al).
            mockRepository = new MockRepository(MockBehavior.Strict)
            {
                // Never invoke the base class implementation.
                CallBase = false,
            };
        }

        /// <summary>
        /// Actually creates the mock and configures it as specified.
        /// </summary>
        public Mock<RestClient> Get()
        {
            var restClient = mockRepository.Create<RestClient>(AccessKey, ProxyConfigurationInjector);
            
            if (ResponseBody == null && ResponseStream == null)
            {
                throw new Exception("Mock must be configured to return a response body or stream.");
            }
            
            if (Method == null || Uri == null)
            {
                throw new Exception("Mock must be configured to expect a HTTP method and URI.");
            }

            // Handle the overload...
            if (ResponseStream != null)
            {
                restClient.Setup(c => c.PerformHttpRequest(Uri, It.IsAny<HttpStatusCode>(), BaseUrl)).Returns(ResponseStream).Verifiable();
            }
            else if (RequestBody == null)
            {
                restClient.Setup(c => c.PerformHttpRequest(Method, Uri, It.IsAny<HttpStatusCode>(), BaseUrl)).Returns(ResponseBody).Verifiable();
            }
            else
            {
                restClient.Setup(c => c.PerformHttpRequest(Method, Uri,
                    It.Is<string>(arg => JToken.DeepEquals(JToken.Parse(RequestBody), JToken.Parse(arg))),
                    It.IsAny<HttpStatusCode>(), BaseUrl)).Returns(ResponseBody).Verifiable();
            }
            
            return restClient;
        }
        
        /// <summary>
        /// Creates a mock client and sets the expected request body.
        /// </summary>
        public static MockRestClient ThatExpects(string requestBody)
        {
            return new MockRestClient { RequestBody = requestBody };
        }

        /// <summary>
        /// Creates a mock client and sets the expected response body.
        /// </summary>
        public static MockRestClient ThatReturns(string responseBody = null, string filename = null, Stream stream = null)
        {
            string mockResponseBody = string.Empty;

            if (!string.IsNullOrEmpty(responseBody))
            {
                mockResponseBody = responseBody;
            }
            else if (!string.IsNullOrEmpty(filename))
            {
                mockResponseBody = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Responses", filename));
            }

            return new MockRestClient
            {
                ResponseBody = mockResponseBody,
                ResponseStream = stream
            };
        }

        /// <summary>
        /// Sets the expected response body from string or json file name.
        /// </summary>
        public MockRestClient AndReturns(string responseBody = null, string filename = null)
        {
            ResponseBody = responseBody ?? File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Responses", filename));

            return this;
        }

        /// <summary>
        /// Sets the expected (HTTP) method.
        /// </summary>
        public MockRestClient FromEndpoint(string method, string uri, string baseUrl = null)
        {
            Method = method;
            Uri = uri;
            BaseUrl = baseUrl ?? Resource.DefaultBaseUrl;

            return this;
        }
    }
}
