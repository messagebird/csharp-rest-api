using System;
using System.IO;
using System.Net;
using System.Text;

using MessageBird.Exceptions;
using MessageBird.Net.ProxyConfigurationInjector;
using MessageBird.Resources;

namespace MessageBird.Net
{
    // immutable, so no read/write properties
    public class RestClient : IRestClient
    {
        public static readonly string HttpsRestMessagebirdComEndpoint = "https://rest.messagebird.com";

        public string AccessKey { get; private set; }

        public string Endpoint { get; private set; }

        public IProxyConfigurationInjector ProxyConfigurationInjector { get; private set; }

        public string ClientVersion
        {
            get { return "1.0"; }
        }

        public string ApiVersion
        {
            get { return "2.0"; }
        }

        public string UserAgent
        {
            get { return string.Format("MessageBird/ApiClient/{0} DotNet/{1}", ApiVersion, ClientVersion); }
        }

        public RestClient(string endpoint, string accessKey, IProxyConfigurationInjector proxyConfigurationInjector)
        {
            Endpoint = endpoint;
            AccessKey = accessKey;
            ProxyConfigurationInjector = proxyConfigurationInjector;
        }

        public RestClient(string accessKey, IProxyConfigurationInjector proxyConfigurationInjector)
            : this(HttpsRestMessagebirdComEndpoint, accessKey, proxyConfigurationInjector)
        {
        }

        public T Retrieve<T>(T resource) where T : Resource
        {
            string uri = resource.HasId ? String.Format("{0}/{1}", resource.Name, resource.Id) : resource.Name;
            HttpWebRequest request = PrepareRequest(uri, "GET");

            return PerformRoundTrip(request, resource, HttpStatusCode.OK, () => { }
            );
        }

        public T Create<T>(T resource) where T : Resource
        {
            HttpWebRequest request = PrepareRequest(resource.Name, "POST");
            return PerformRoundTrip(request, resource, HttpStatusCode.Created, () =>
            {
                using (var requestWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string serializedResource = resource.Serialize();
                    requestWriter.Write(serializedResource);
                }
            }
            );
        }

        private T PerformRoundTrip<T>(HttpWebRequest request, T resource, HttpStatusCode expectedHttpStatusCode, Action requestAction)
            where T : Resource
        {
            try
            {
                requestAction();

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    var statusCode = (HttpStatusCode)response.StatusCode;
                    if (statusCode == expectedHttpStatusCode)
                    {
                        Stream responseStream = response.GetResponseStream();
                        Encoding encoding = GetEncoding(response);

                        using (var responseReader = new StreamReader(responseStream, encoding))
                        {
                            string responseContent = responseReader.ReadToEnd();
                            resource.Deserialize(responseContent);
                            return resource;
                        }
                    }
                    throw new ErrorException(String.Format("Unexpected status code {0}", statusCode));
                }
            }
            catch (WebException e)
            {
                throw ErrorExceptionFromWebException(e);
            }
            catch (Exception e)
            {
                throw new ErrorException(String.Format("Unhandled exception {0}", e), e);
            }
        }

        private static Encoding GetEncoding(HttpWebResponse response)
        {
            // TODO: Make this conditional on the encoding of the response.
            Encoding encode = Encoding.UTF8; // GetEncoding("utf-8"); // Encoding.GetEncoding(response.CharacterSet);
            return encode;
        }

        public void Update(Resource resource)
        {
            throw new NotImplementedException();
        }

        public void Delete(Resource resource)
        {
            throw new NotImplementedException();
        }

        private HttpWebRequest PrepareRequest(string requestUriString, string method)
        {
            string uriString = String.Format("{0}/{1}", Endpoint, requestUriString);
            var uri = new Uri(uriString);
            // TODO: ##jwp; need to find out why .NET 4.0 under VS2013 refuses to recognize `WebRequest.CreateHttp`.
            // HttpWebRequest request = WebRequest.CreateHttp(uri);
            var request = WebRequest.Create(uri) as HttpWebRequest;
            request.UserAgent = UserAgent;
            const string ApplicationJsonContentType = "application/json"; // http://tools.ietf.org/html/rfc4627
            request.Accept = ApplicationJsonContentType;
            request.ContentType = ApplicationJsonContentType;
            request.Method = method;

            WebHeaderCollection headers = request.Headers;
            headers.Add("Authorization", String.Format("AccessKey {0}", AccessKey));

            if (null != ProxyConfigurationInjector)
            {
                request.Proxy = ProxyConfigurationInjector.InjectProxyConfiguration(request.Proxy, uri);
            }
            return request;
        }

        private ErrorException ErrorExceptionFromWebException(WebException e)
        {
            var httpWebResponse = e.Response as HttpWebResponse;
            if (null == httpWebResponse)
            {
                // some kind of network error: didn't even make a connection
                return new ErrorException(e.Message, e);
            }

            var statusCode = (HttpStatusCode)httpWebResponse.StatusCode;
            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.MethodNotAllowed:
                case HttpStatusCode.UnprocessableEntity:
                    using (var responseReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        ErrorException errorException = ErrorException.FromResponse(responseReader.ReadToEnd(), e);
                        if (errorException != null)
                        {
                            return errorException;
                        }
                        return new ErrorException(String.Format("Unknown error for {0}", statusCode), e);
                    }
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.NotImplemented:
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.GatewayTimeout:
                case HttpStatusCode.HttpVersionNotSupported:
                case HttpStatusCode.VariantAlsoNegotiates:
                case HttpStatusCode.InsufficientStorage:
                case HttpStatusCode.LoopDetected:
                case HttpStatusCode.BandwidthLimitExceeded:
                case HttpStatusCode.NotExtended:
                case HttpStatusCode.NetworkAuthenticationRequired:
                case HttpStatusCode.NetworkReadTimeoutError:
                case HttpStatusCode.NetworkConnectTimeoutError:
                    return new ErrorException("Something went wrong on our end, please try again", e);
                default:
                    return new ErrorException(String.Format("Unhandled status code {0}", statusCode), e);
            }
        }
    }
}
