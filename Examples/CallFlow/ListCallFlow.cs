using MessageBird;
using MessageBird.Exceptions;
using System;

namespace Examples.CallFlow
{
    internal class ListCallFlow
    {
        const string YOUR_ACCESS_KEY = "YOUR_ACCESS_KEY";

        internal static void Main(string[] args)
        {
            var client = Client.CreateDefault(YOUR_ACCESS_KEY);

            var callFlowList = client.ListCallFlows();
            try
            {
                foreach (var item in callFlowList.Data)
                {
                    Console.WriteLine("The Voice Call Flow Id is: {0}", item.Id);
                    Console.WriteLine("The Voice Call Flow Title is: {0}", item.Title);
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
