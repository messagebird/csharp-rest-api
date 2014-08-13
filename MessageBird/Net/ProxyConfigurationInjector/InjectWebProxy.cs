using System;
using System.Net;

namespace MessageBird.Net.ProxyConfigurationInjector
{
    /// <summary>
    /// Completely overwrites the web proxy so you can hard configure it. Example:
    /// <code>
    /// ICredentials credentials = new NetworkCredential("xxxx", "xxxx");
    /// IWebProxy webProxy = new WebProxy (new Uri("http://xx.xx.xx.xxx:xxxx"));
    /// webProxy.Credentials = credentials;
    /// new InjectWebProxy(webProxy);
    /// </code>
    /// </summary>
    public class InjectWebProxy: IProxyConfigurationInjector
    {
        private readonly IWebProxy webProxyToInject;

        public InjectWebProxy(IWebProxy webProxyToInject)
        {
            this.webProxyToInject = webProxyToInject;
        }

        public IWebProxy InjectProxyConfiguration(IWebProxy webProxy, Uri uri)
        {
            return webProxyToInject;
        }
    }
}