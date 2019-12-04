using System;
using System.Linq;
using MessageBird;
using MessageBird.Exceptions;

namespace Examples.VoiceCallFlow
{
    internal class ViewVoiceCallFlow
    {
        const string YOUR_ACCESS_KEY = "YOUR_ACCESS_KEY";

        internal static void Main(string[] args)
        {
            var client = Client.CreateDefault(YOUR_ACCESS_KEY);

            try
            {
                var voiceCallFlowResponse = client.ViewVoiceCallFlow("PUT YOUR REQUEST ID HERE");
                var voiceCallFlow = voiceCallFlowResponse.Data.FirstOrDefault();

                Console.WriteLine("The Voice Call Flow Id is: {0}", voiceCallFlow.Id);
                Console.WriteLine("The Voice Call Flow Title is: {0}", voiceCallFlow.Title);
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
