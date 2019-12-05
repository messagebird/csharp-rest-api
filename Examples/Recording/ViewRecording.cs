using System;
using System.Linq;
using MessageBird;
using MessageBird.Exceptions;

namespace Examples.Recording
{ 
    internal class ViewRecording
    {
        const string YOUR_ACCESS_KEY = "YOUR_ACCESS_KEY";

        internal static void Main(string[] args)
        {
            var client = Client.CreateDefault(YOUR_ACCESS_KEY);

            try
            {
                var recordingResponse = client.ViewRecording("CALL ID", "LEG ID", "RECORDING ID");
                var recording = recordingResponse.Data.FirstOrDefault();

                Console.WriteLine("The Recording Id is: {0}", recording.Id);
                Console.WriteLine("The Recording Format is: {0}", recording.Format);
                Console.WriteLine("The Recording Duration is: {0}", recording.Duration);
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
