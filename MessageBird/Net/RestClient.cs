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
            get { return "1.4.0.0"; }
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

        /// <summary>
        /// Retrieves a resource with HTTP GET.
        /// </summary>
        public T Retrieve<T>(T resource) where T : Resource
        {
            var uri = GetUriWithQueryString(resource);

            return RequestWithResource("GET", uri, resource, HttpStatusCode.OK);
        }

        /// <summary>
        /// Creates a resource with HTTP POST.
        /// </summary>
        public T Create<T>(T resource) where T : Resource
        {
            var uri = GetUriWithQueryString(resource);

            return RequestWithResource("POST", uri, resource, HttpStatusCode.Created);
        }
        
        /// <summary>
        /// Updates a resource. HTTP method is determined by
        /// RestClientOptions.UpdateMode.
        /// </summary>
        public void Update(Resource resource)
        {
            var method = GetUpdateMethod();
            var uri = GetUriWithQueryString(resource);

            RequestWithResource(method, uri, resource, HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Determines what HTTP method should be used for updates: some API's
        /// use PATCH and others require PUT. We can unfortunately not just add
        /// a Patch method or set this property on this class. That would be a
        /// breaking change: we can't just change the public interface.
        /// </summary>
        private string GetUpdateMethod()
        {
            if (RestClientOptions.UpdateMode == UpdateMode.Patch)
            {
                return "PATCH";
            }
            else if (RestClientOptions.UpdateMode == UpdateMode.Put)
            {
                return "PUT";
            }

            throw new Exception("Unexpected UpdateMode: " + RestClientOptions.UpdateMode);
        }

        public void Delete(Resource resource)
        {
            var uri = GetUriWithQueryString(resource);

            PerformHttpRequest("DELETE", uri, HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Gets the resource's URI with the query string, if it is set.
        /// </summary>
        private string GetUriWithQueryString(Resource resource)
        {
            var uri = resource.Uri;
            if (resource.HasQueryString)
            {
                uri += "?" + resource.QueryString;
            }
            return uri;
        }

        /// <summary>
        /// Performs a HTTP request and does any (de)serialization needed.
        /// </summary>
        private T RequestWithResource<T>(string method, string uri, T resource, HttpStatusCode expectedHttpStatusCode)
            where T : Resource
        {
            string response;

            if (method == "GET" || method == "DELETE")
            {
                response = PerformHttpRequest(method, uri, expectedHttpStatusCode);
            }
            else
            {
                string s = resource.Serialize();
                response = PerformHttpRequest(method, uri, s, expectedHttpStatusCode);
            }

            resource.Deserialize(response);

            return resource;
        }

        private static Encoding GetEncoding(HttpWebResponse response)
        {
            // TODO: Make this conditional on the encoding of the response.
            Encoding encode = Encoding.UTF8; // GetEncoding("utf-8"); // Encoding.GetEncoding(response.CharacterSet);
            return encode;
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

        public virtual string PerformHttpRequest(string method, string uri, string body, HttpStatusCode expectedStatusCode)
        {
            var request = PrepareRequest(method, uri);

            try
            {
                if (!string.IsNullOrEmpty(body))
                {
                    using (var requestWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        requestWriter.Write(body);
                    }
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    var statusCode = (HttpStatusCode)response.StatusCode;
                    if (statusCode == expectedStatusCode)
                    {
                        Stream responseStream = response.GetResponseStream();
                        Encoding encoding = GetEncoding(response);

                        using (var responseReader = new StreamReader(responseStream, encoding))
                        {
                            return responseReader.ReadToEnd();
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

        public virtual string PerformHttpRequest(string method, string uri, HttpStatusCode expectedStatusCode)
        {
            string body = null;

            return PerformHttpRequest(method, uri, body, expectedStatusCode);
        }

        private HttpWebRequest PrepareRequest(string method, string requestUriString)
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
    }
}
