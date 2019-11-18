using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Objects.VoiceCalls;
using System;

namespace Examples.VoiceCallFlow
{
    internal class CreateVoiceCallFlow
    {
        private const string YourAccessKey = "YOUR_ACCESS_KEY";

        internal static void Main(string[] args)
        {
            var client = Client.CreateDefault(YourAccessKey);
            var newVoiceCallFlow = new MessageBird.Objects.VoiceCalls.VoiceCallFlow
            {
                Title = "Forward call to 1234567890",
                Record = true
            };
            newVoiceCallFlow.Steps.Add(new Step { Action = "transfer", Options = new Options { Destination = "1234567890" } });
            try
            {
                var voiceCallFlow = client.CreateVoiceCallFlow(newVoiceCallFlow);
                Console.WriteLine("The Voice Call Flow Created with Id = {0}", voiceCallFlow.Id);
            }
            catch (ErrorException e)
            {
                // Either the request fails with error descriptions from the endpoint.
                if (e.HasErrors)
                {
                    foreach (var error in e.Errors)
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
