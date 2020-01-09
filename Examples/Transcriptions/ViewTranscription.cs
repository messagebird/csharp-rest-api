using System;
using System.Linq;
using MessageBird;
using MessageBird.Exceptions;

namespace Examples.Transcription
{ 
    internal class ViewTranscription
    {
        const string YOUR_ACCESS_KEY = "YOUR_ACCESS_KEY";

        internal static void Main(string[] args)
        {
            var client = Client.CreateDefault(YOUR_ACCESS_KEY);

            try
            {
                var transcriptionResponse = client.ViewTranscription("CALL ID", "LEG ID", "RECORDING ID", "TRANSCRIPTION ID");
                var transcription = transcriptionResponse.Data.FirstOrDefault();

                Console.WriteLine("The Transcription Id is: {0}", transcription.Id);
                Console.WriteLine("The Transcription Recording Id is: {0}", transcription.RecordingId);
                Console.WriteLine("The Transcription Status is: {0}", transcription.Status);
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
