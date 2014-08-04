using System.Net;
using MessageBird.Resources;
using MessageBird.Exceptions;
using System;
using System.IO;
using System.Text;

namespace MessageBird.Net
{
    public interface IRestClient
    {
        string Endpoint { get; set; }
        string ClientVersion { get; }
        string ApiVersion { get; }
        string UserAgent { get; }
        string AccessKey { get; set; }

        Resource Create(Resource resource);
        Resource Retrieve(Resource resource);
        void Update(Resource resource);
        void Delete(Resource resource);
    }

    public class RestClient : IRestClient
    {
        public string Endpoint {get; set;}
        public string ClientVersion { get { return "1.0"; } }
        public string ApiVersion { get { return "2.0";  } }
        public string UserAgent { get { return string.Format("MessageBird/ApiClient/{0} DotNet/{1}", ApiVersion, ClientVersion); } }
        public string AccessKey { get; set; }
        
        public RestClient(string endpoint, string accessKey)
        {
            Endpoint = endpoint;
            AccessKey = accessKey;
        }

        public RestClient(string accessKey) : this("https://rest.messagebird.com", accessKey)
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
                            throw new ErrorException(String.Format("Unexpected status code {0}", statusCode));
                    }
                }
            }
            catch (WebException e)
            {
                throw ErrorExceptionFromWebException(e);
            }
            catch (Exception e)
            {
                throw new ErrorException(String.Format("Unhandled exception {0}", e));
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
                            throw new ErrorException(String.Format("Unexpected status code {0}", statusCode));
                    }
                }
            }
            catch (WebException e)
            {
                throw ErrorExceptionFromWebException(e);
            }
            catch (Exception e)
            {
                throw new ErrorException(String.Format("Unhandled exception {0}", e));
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

        private HttpWebRequest PrepareRequest(string requestUri, string method)
        {
            HttpWebRequest request = WebRequest.CreateHttp(String.Format("{0}/{1}",Endpoint, requestUri));
            request.UserAgent = UserAgent;
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Method = method;

            WebHeaderCollection headers = request.Headers;
            headers.Add("Authorization", String.Format("AccessKey {0}", AccessKey));

            return request;
        }

        private ErrorException ErrorExceptionFromWebException(WebException e)
        {
            HttpStatusCode statusCode = (HttpStatusCode)((HttpWebResponse)e.Response).StatusCode;
            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.MethodNotAllowed:
                case HttpStatusCode.UnprocessableEntity:
                    using (StreamReader responseReader = new StreamReader(e.Response.GetResponseStream()))
                    {
                        ErrorException errorException = ErrorException.FromResponse(responseReader.ReadToEnd());
                        if (errorException != null)
                        {
                            return errorException;
                        }
                        else
                        {
                            return new ErrorException(String.Format("Unknown error for {0}", statusCode));
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
                    return new ErrorException("Something went wrong on our end, please try again");
                default:
                    return new ErrorException(String.Format("Unhandled status code {0}", statusCode));
            }
        }
    }
}
