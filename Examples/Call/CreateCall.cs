using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Objects.Voice;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Examples.Call
{
    internal class CreateCall
    {
        const string YourAccessKey = "YOUR_ACCESS_KEY"; // your access key here.

        internal static void Main(string[] args)
        {

            var client = Client.CreateDefault(YourAccessKey);
            var newCallFlow = new CallFlow
            {
                Title = "Forward call to 31612345678",
                Record = true,
                Steps = new List<Step> { new Step { Action = "transfer", Options = new Options { Destination = "31612345678" } } }
            };

            var newCall = new Call
            {
                source = "31644556677",
                destination = "33766723144",
                callFlow = newCallFlow

            };
            try
            {
                var callResponse = client.CreateCall(newCall);
                var call = callResponse.Data.FirstOrDefault();
                Console.WriteLine("The Call Flow Created with Id = {0}", call.Id);
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
