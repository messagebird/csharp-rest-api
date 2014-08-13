using System;
using System.Net;

namespace MessageBird.Net.ProxyConfigurationInjector
{
    public interface IProxyConfigurationInjector
    {
        IWebProxy InjectProxyConfiguration(IWebProxy webProxy, Uri uri);
    }
}