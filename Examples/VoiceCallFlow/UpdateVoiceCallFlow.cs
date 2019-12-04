using System;
using System.Linq;
using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Objects.VoiceCalls;

namespace Examples.VoiceCallFlow
{
    internal class UpdateVoiceCallFlow
    {
        const string YOUR_ACCESS_KEY = "YOUR_ACCESS_KEY";

        internal static void Main(string[] args)
        {
            var client = Client.CreateDefault(YOUR_ACCESS_KEY);
            var voiceCallFlow = new MessageBird.Objects.VoiceCalls.VoiceCallFlow
            {
                Title = "PUT YOUR TITLE HERE",
                Record = true
            };
            voiceCallFlow.Steps.Add(new Step { Action = "transfer", Options = new Options { Destination = "1234567890" } });
            
            try
            {
                var voiceCallFlowResponse = client.UpdateVoiceCallFlow("ID", voiceCallFlow);
                var updatedVoiceCallFlow = voiceCallFlowResponse.Data.FirstOrDefault();

                Console.WriteLine("The Voice Call Flow with Id = {0} has been updated", updatedVoiceCallFlow.Id);
                Console.WriteLine("The Voice Call Flow Title is: {0}", updatedVoiceCallFlow.Title);
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
