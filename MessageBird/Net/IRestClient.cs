using System.IO;
using MessageBird.Net.ProxyConfigurationInjector;
using MessageBird.Resources;

namespace MessageBird.Net
{
    // immutable, so no read/write properties
    public interface IRestClient
    {
        string AccessKey { get; }
        string Endpoint { get; }
        IProxyConfigurationInjector ProxyConfigurationInjector { get; }

        string ApiVersion { get; }
        string ClientVersion { get; }
        string UserAgent { get; }

        T Create<T> (T resource) where T : Resource;
        T Retrieve<T>(T resource) where T : Resource;
        T Update<T>(T resource) where T : Resource;
        void Delete(Resource resource);

        string PerformHttpRequest(string method, string uri, string body, HttpStatusCode expectedStatusCode);
        string PerformHttpRequest(string method, string uri, HttpStatusCode expectedStatusCode);
        string PerformHttpRequest(string method, string uri, string body, HttpStatusCode expectedStatusCode, string baseUrl);
        string PerformHttpRequest(string method, string uri, HttpStatusCode expectedStatusCode, string baseUrl);
        Stream PerformHttpRequest(string uri, HttpStatusCode expectedStatusCode, string baseUrl);
    }
}