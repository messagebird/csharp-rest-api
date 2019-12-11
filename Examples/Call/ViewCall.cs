using System;
using System.Linq;
using MessageBird;
using MessageBird.Exceptions;

namespace Examples.Call
{ 
    internal class ViewCall
    {
        const string YOUR_ACCESS_KEY = "YOUR_ACCESS_KEY";

        internal static void Main(string[] args)
        {
            var client = Client.CreateDefault(YOUR_ACCESS_KEY);

            try
            {
                var callResponse = client.ViewCall("CALL ID");
                var call = callResponse.Data.FirstOrDefault();

                Console.WriteLine("The Call Id is: {0}", call.Id);
                Console.WriteLine("The Call source is: {0}", call.Source);
                Console.WriteLine("The Call destination is: {0}", call.Destination);
                Console.WriteLine("The Call ended at: {0}", call.EndedAt);
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