using System;
using System.IO;
using System.Net;
using System.Text;

using MessageBird.Exceptions;
using MessageBird.Resources;

namespace MessageBird.Net
{
    // immutable, so no read/write properties
    public interface IRestClient
    {
        string AccessKey { get; }
        string Endpoint { get; }
        ICredentials ProxyCredentials { get; }

        string ApiVersion { get; }
        string ClientVersion { get; }
        string UserAgent { get; }

        Resource Create(Resource resource);
        Resource Retrieve(Resource resource);
        void Update(Resource resource);
        void Delete(Resource resource);
    }

    internal class RestClient : IRestClient
    {
        public string AccessKey { get; private set; }

        public string Endpoint { get; private set; }

        public ICredentials ProxyCredentials { get; private set; }

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

        public RestClient(string endpoint, string accessKey, ICredentials proxyCredentials)
        {
            Endpoint = endpoint;
            AccessKey = accessKey;
            ProxyCredentials = proxyCredentials;
        }

        public RestClient(string accessKey, ICredentials proxyCredentials)
            : this("https://rest.messagebird.com", accessKey, proxyCredentials)
        {
        }

        public Resource Retrieve(Resource resource)
        {
            string uri = resource.HasId ? String.Format("{0}/{1}", resource.Name, resource.Id) : resource.Name;
            HttpWebRequest request = PrepareRequest(uri, "GET");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    HttpStatusCode statusCode = (HttpStatusCode)response.StatusCode;
                    switch (statusCode)
                    {
                        case MessageBird.Net.HttpStatusCode.OK:
                            Stream responseStream = response.GetResponseStream();
                            // XXX: Makes this conditional on the encoding of the response.
                            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

                            using (StreamReader responseReader = new StreamReader(responseStream, encode))
                            {
                                resource.Deserialize(responseReader.ReadToEnd());
                                return resource;
                            }
                        default:
                            throw new ErrorException(String.Format("Unexpected status code {0}", statusCode), null);
                    }
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

        public Resource Create(Resource resource)
        {
            HttpWebRequest request = PrepareRequest(resource.Name, "POST");
            try
            {
                using (StreamWriter requestWriter = new StreamWriter(request.GetRequestStream()))
                {
                    requestWriter.Write(resource.Serialize());
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    HttpStatusCode statusCode = (HttpStatusCode)response.StatusCode;
                    switch (statusCode)
                    {
                        case MessageBird.Net.HttpStatusCode.Created:
                            Stream responseStream = response.GetResponseStream();
                            // XXX: Makes this conditional on the encoding of the response.
                            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

                            using (StreamReader responseReader = new StreamReader(responseStream, encode))
                            {
                                resource.Deserialize(responseReader.ReadToEnd());
                                return resource;
                            }
                        default:
                            throw new ErrorException(String.Format("Unexpected status code {0}", statusCode), null);
                    }
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
            Uri uri = new Uri(uriString);
            // TODO: ##jwp; need to find out why .NET 4.0 under VS2013 refuses to recognize `WebRequest.CreateHttp`.
            // HttpWebRequest request = WebRequest.CreateHttp(uri);
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            request.UserAgent = UserAgent;
            const string ApplicationJsonContentType = "application/json"; // http://tools.ietf.org/html/rfc4627
            request.Accept = ApplicationJsonContentType;
            request.ContentType = ApplicationJsonContentType;
            request.Method = method;

            WebHeaderCollection headers = request.Headers;
            headers.Add("Authorization", String.Format("AccessKey {0}", AccessKey));

            Uri proxy = WebRequest.DefaultWebProxy.GetProxy(uri);
            if (uri != proxy) // request goes through proxy
            {
                IWebProxy webProxy = request.Proxy;
                // webProxy.UseDefaultCredentials = true; // not accessible through IWebProxy
                // webProxy.Credentials = CredentialCache.DefaultCredentials; // same as setting `webProxy.UseDefaultCredentials = true`
                webProxy.Credentials = ProxyCredentials; // better to pass it as a dependency
            }

            return request;
        }

        private ErrorException ErrorExceptionFromWebException(WebException e)
        {
            HttpWebResponse httpWebResponse = (HttpWebResponse)e.Response;
            if (null == httpWebResponse)
            {
                // some kind of network error: didn't even make a connection
                return new ErrorException(e.Message, e);
            }

            HttpStatusCode statusCode = (HttpStatusCode)httpWebResponse.StatusCode;
            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.MethodNotAllowed:
                case HttpStatusCode.UnprocessableEntity:
                    using (StreamReader responseReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        ErrorException errorException = ErrorException.FromResponse(responseReader.ReadToEnd(), e);
                        if (errorException != null)
                        {
                            return errorException;
                        }
                        else
                        {
                            return new ErrorException(String.Format("Unknown error for {0}", statusCode), e);
                        }
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
