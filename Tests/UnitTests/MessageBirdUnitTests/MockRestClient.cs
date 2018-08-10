using System;

using MessageBird.Net;
using MessageBird.Net.ProxyConfigurationInjector;
using Moq;

namespace MessageBirdTests
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
        private string Method { get; set; }
        private string Uri { get; set; }

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
            
            if (ResponseBody == null)
            {
                throw new Exception("Mock must be configured to return a response body.");
            }
            
            if (Method == null || Uri == null)
            {
                throw new Exception("Mock must be configured to expect a HTTP method and URI.");
            }

            // Handle the overload...
            if (RequestBody == null)
            {
                restClient.Setup(c => c.PerformHttpRequest(Method, Uri, It.IsAny<HttpStatusCode>())).Returns(ResponseBody).Verifiable();
            }
            else
            {
                restClient.Setup(c => c.PerformHttpRequest(Method, Uri, It.IsAny<string>(), It.IsAny<HttpStatusCode>())).Returns(ResponseBody).Verifiable();
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
        public static MockRestClient ThatReturns(string responseBody)
        {
            return new MockRestClient { ResponseBody = responseBody };
        }

        /// <summary>
        /// Sets the expected response body.
        /// </summary>
        public MockRestClient AndReturns(string responseBody)
        {
            ResponseBody = responseBody;

            return this;
        }

        /// <summary>
        /// Sets the expected (HTTP) method.
        /// </summary>
        public MockRestClient FromEndpoint(string method, string uri)
        {
            Method = method;
            Uri = uri;

            return this;
        }
    }
}
