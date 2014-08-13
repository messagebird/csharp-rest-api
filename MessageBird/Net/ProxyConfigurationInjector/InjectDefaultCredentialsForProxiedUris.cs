using System.Net;

namespace MessageBird.Net.ProxyConfigurationInjector
{
    public class InjectDefaultCredentialsForProxiedUris: InjectCredentialsForProxiedUris 
    {
        public InjectDefaultCredentialsForProxiedUris(): base (CredentialCache.DefaultCredentials)
        {
        }
    }
}
