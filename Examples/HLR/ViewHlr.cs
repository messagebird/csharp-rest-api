using System;
using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Net.ProxyConfigurationInjector;
using MessageBird.Objects;

namespace Examples.HLR
{
    class ViewHlr
    {
        const string YourAccessKey = "YOUR_ACCESS_KEY"; // your access key here.
        const string HlrId = "c8143db0152a58755c80492h61377581"; // ID of HLR you obtained before (seems to require also "in the same session"). When not found, you will not get a http StatusCode 404, but an exception `code: 20 description: 'hlr not found' parameter: ''`

        static void Main(string[] args)
        {
            IProxyConfigurationInjector proxyConfigurationInjector = null; // for no web proxies, or web proxies not requiring authentication
            //proxyConfigurationInjector = new InjectDefaultCredentialsForProxiedUris(); // for NTLM based web proxies
            //proxyConfigurationInjector = new InjectCredentialsForProxiedUris(new NetworkCredential("domain\\user", "password")); // for username/password based web proxies

            Client client = Client.CreateDefault(YourAccessKey, proxyConfigurationInjector);

            try
            {
                Hlr hlr = client.ViewHlr(HlrId);
                Console.WriteLine("{0}", hlr);

            }
            catch (ErrorException e)
            {
                // Either the request fails with error descriptions from the endpoint.
                if (e.HasErrors)
                {
                    foreach (Error error in e.Errors)
                    {
                        Console.WriteLine("code: {0} description: '{1}' parameter: '{2}'", error.Code, error.Description, error.Parameter);
                    }
                }
                // or fails without error information from the endpoint, in which case the reason contains a 'best effort' description.
                if (e.HasReason)
                {
                    Console.WriteLine(e.Reason);
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
