﻿using System;
using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Net.ProxyConfigurationInjector;
using MessageBird.Objects;

namespace Examples.VoiceMessage
{
    class CreateVoiceMessage
    {
        const string YourAccessKey = "YOUR_ACCESS_KEY"; // your access key here.
        const long Msisdn = 31612345678; // your phone number here.

        static void Main(string[] args)
        {
            IProxyConfigurationInjector proxyConfigurationInjector = null; // for no web proxies, or web proxies not requiring authentication
            //proxyConfigurationInjector = new InjectDefaultCredentialsForProxiedUris(); // for NTLM based web proxies
            //proxyConfigurationInjector = new InjectCredentialsForProxiedUris(new NetworkCredential("domain\\user", "password")); // for username/password based web proxies

            Client client = Client.CreateDefault(YourAccessKey, proxyConfigurationInjector);

            try
            {
                var optionalArguments = new VoiceMessageOptionalArguments
                {
                    Language = Language.English,
                    Voice = Voice.Female,
                    IfMachine = IfMachineOptions.Continue

                };
                MessageBird.Objects.VoiceMessage voiceMessage = client.SendVoiceMessage("This is a test message. The message is converted to speech and the recipient is called on his mobile.", new[] { Msisdn }, optionalArguments);
                Console.WriteLine("{0}", voiceMessage);
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
