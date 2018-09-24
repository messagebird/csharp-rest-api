﻿using MessageBird.Net.ProxyConfigurationInjector;
using MessageBird.Resources;
using System;

namespace MessageBird.Net
{
    // immutable, so no read/write properties
    public interface IRestClient
    {
        string AccessKey { get; }
        string Endpoint { get; }
        IProxyConfigurationInjector ProxyConfigurationInjector { get; }
        TimeSpan RequestTimeout { get; set; }

        string ApiVersion { get; }
        string ClientVersion { get; }
        string UserAgent { get; }

        T Create<T> (T resource) where T : Resource;
        T Retrieve<T>(T resource) where T : Resource;
        void Update(Resource resource);
        void Delete(Resource resource);
    }
}