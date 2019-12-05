using MessageBird;
using MessageBird.Exceptions;
using System;
using System.IO;

namespace Examples.Recording
{
    internal class DownloadRecording
    {
        const string YOUR_ACCESS_KEY = "YOUR_ACCESS_KEY";

        internal static void Main(string[] args)
        {
            var client = Client.CreateDefault(YOUR_ACCESS_KEY);

            try
            {
                using (var recordingDataStream = client.DownloadRecording("CALL ID", "LEG ID", "RECORDING ID"))
                { 
                    using (var fileStream = File.OpenWrite(@"PATH TO FILE ON YOUR LOCAL MACHINE"))
                    {
                        recordingDataStream.CopyTo(fileStream);
                    }
                }
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
