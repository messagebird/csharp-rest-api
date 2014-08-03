using System.Net;
using MessageBird.Resources;
using System;
using System.IO;
using System.Text;

namespace MessageBird.Net
{
    public class UnexpectedStatusCodeException : Exception {}

    public interface IRestClient
    {
        string Endpoint { get; set; }
        string ClientVersion { get; }
        string ApiVersion { get; }
        string UserAgent { get; }
        string AccessKey { get; set; }

        IResource Create(IResource resource);
        IResource Retrieve(IResource resource);
        void Update(IResource resource);
        void Delete(IResource resource);
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

        public RestClient(string accessKey) : this("https://rest.messagebird.com/", accessKey)
        {
        }

        public IResource Retrieve(IResource resource)
        {
            string uri = String.Format("{0}/{1}", resource.Name, resource.Id);
            HttpWebRequest request = PrepareRequest(uri, "GET");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    switch ((MessageBird.Net.HttpStatusCode)response.StatusCode)
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
                            throw new UnexpectedStatusCodeException();
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                // XXX: Need to handle this exception properly.
                throw e;
            }
        }

        public IResource Create(IResource resource)
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
                    switch ((MessageBird.Net.HttpStatusCode)response.StatusCode)
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
                            throw new UnexpectedStatusCodeException();
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                // XXX: Need to handle this exception properly.
                throw e;
            }
        }

        public void Update(IResource resource)
        {
            throw new NotImplementedException();
        }

        public void Delete(IResource resource)
        {
            throw new NotImplementedException();
        }

        private HttpWebRequest PrepareRequest(string requestUri, string method)
        {
            HttpWebRequest request = WebRequest.CreateHttp(String.Format("{0}/{1}",Endpoint, requestUri));
            request.UserAgent = UserAgent;
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.TransferEncoding = "utf-8";
            request.Method = method;

            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Authorization", String.Format("AccessKey {0}", AccessKey));
            request.Headers = headers;

            return request;
        }
    }
}
