using System;
using System.Net;

namespace MessageBird.Net.ProxyConfigurationInjector
{
    public class InjectCredentialsForProxiedUris : IProxyConfigurationInjector
    {
        private readonly ICredentials credentials;

        public InjectCredentialsForProxiedUris(ICredentials credentials)
        {
            this.credentials = credentials;
        }

        public IWebProxy InjectProxyConfiguration(IWebProxy webProxy, Uri uri)
        {
            Uri proxy = WebRequest.DefaultWebProxy.GetProxy(uri);
            if (uri != proxy) // request goes through proxy
            {
                // webProxy.UseDefaultCredentials = true; // not accessible through IWebProxy
                webProxy.Credentials = credentials; // same as setting `webProxy.UseDefaultCredentials = true`
            }
            return webProxy;
        }
    }
}