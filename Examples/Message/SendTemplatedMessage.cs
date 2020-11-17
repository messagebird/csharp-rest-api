using System;
using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Net.ProxyConfigurationInjector;
using MessageBird.Objects;
using MessageBird.Objects.Conversations;

namespace Examples.Message
{
    class SendTemplatedMessage
    {
        const string YourAccessKey = "YOUR_ACCESS_KEY"; // your access key here.
        const string ChannelId = "YOUR_CHANNEL_ID"; // your channel id here
        const string To = "RECEIVER_PHONE_NUMBER"; // receiver phone number here
        const string HsmNamespace = "HSM_NAMESPACE"; // HSM namespace here
        const string TemplateName = "TEMPLATE_NAME"; // template name here
        const string HsmLanguageCode = "HSM_LANGUAGE_CODE"; // HSM language code


        static void Main(string[] args)
        {
            IProxyConfigurationInjector proxyConfigurationInjector = null; // for no web proxies, or web proxies not requiring authentication
            //proxyConfigurationInjector = new InjectDefaultCredentialsForProxiedUris(); // for NTLM based web proxies
            //proxyConfigurationInjector = new InjectCredentialsForProxiedUris(new NetworkCredential("domain\\user", "password")); // for username/password based web proxies

            Client client = Client.CreateDefault(YourAccessKey, proxyConfigurationInjector);

            try
            {
                Conversation conversation =client.StartConversation(new ConversationStartRequest() 
                { 
                    ChannelId = ChannelId, 
                    To = To, 
                    Type = ContentType.Hsm, 
                    Content = new Content()
                    {
                        Hsm = new HsmContent()
                        {
                            Namespace = HsmNamespace,
                            TemplateName = TemplateName,
                            Language = new HsmLanguage()
                            {
                                Code = HsmLanguageCode,
                                Policy = HsmLanguagePolicy.Deterministic
                            },
                            Params = new System.Collections.Generic.List<HsmLocalizableParameter>()
                            {
                                new HsmLocalizableParameter()
                                {
                                    Default = "Bob"
                                },
                                new HsmLocalizableParameter()
                                {
                                    Default = "tomorrow!"
                                }
                            }
                        }
                    }
                });
                Console.WriteLine("{0}", conversation);
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
