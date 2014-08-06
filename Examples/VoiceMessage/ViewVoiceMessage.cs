using System;
using System.Net;
using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Objects;

namespace Examples.VoiceMessage
{
    class ViewVoiceMessage
    {
        const string YourAccessKey = "YOUR_ACCESS_KEY"; // your access key here.

        static void Main(string[] args)
        {
            Client client = Client.CreateDefault(YourAccessKey);

            try
            {
                MessageBird.Objects.VoiceMessage voiceMessage = client.ViewVoiceMessage("ca0a8220453bc36ddeb3115a37400870");
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
