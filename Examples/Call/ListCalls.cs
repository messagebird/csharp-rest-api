using MessageBird;
using MessageBird.Exceptions;
using System;

namespace Examples.Call
{
    internal class ListCalls
    {
        const string YOUR_ACCESS_KEY = "YOUR_ACCESS_KEY";

        internal static void Main(string[] args)
        {
            var client = Client.CreateDefault(YOUR_ACCESS_KEY);

            var calls = client.ListCalls();
            try
            {
                foreach (var item in calls.Data)
                {
                    Console.WriteLine("The Call Id is: {0}", item.Id);
                    Console.WriteLine("The Call source is: {0}", item.Source);
                    Console.WriteLine("The Call destination is: {0}", item.Destination);
                    Console.WriteLine("The Call ended at: {0}", item.EndedAt);
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
        }
    }
}