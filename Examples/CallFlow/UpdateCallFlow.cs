using System;
using System.Linq;
using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Objects.Voice;

namespace Examples.CallFlow
{
    internal class UpdateCallFlow
    {
        const string YOUR_ACCESS_KEY = "YOUR_ACCESS_KEY";

        internal static void Main(string[] args)
        {
            var client = Client.CreateDefault(YOUR_ACCESS_KEY);
            var callFlow = new MessageBird.Objects.Voice.CallFlow
            {
                Title = "PUT YOUR TITLE HERE",
                Record = true
            };
            callFlow.Steps.Add(new Step { Action = "transfer", Options = new Options { Destination = "1234567890" } });
            
            try
            {
                var callFlowResponse = client.UpdateCallFlow("ID", callFlow);
                var updatedCallFlow = callFlowResponse.Data.FirstOrDefault();

                Console.WriteLine("The Voice Call Flow with Id = {0} has been updated", updatedCallFlow.Id);
                Console.WriteLine("The Voice Call Flow Title is: {0}", updatedCallFlow.Title);
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
