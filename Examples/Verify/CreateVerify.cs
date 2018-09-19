using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Objects;
using System;

namespace Examples.Verify
{
    class CreateVerify
    {
        const string YourAccessKey = "YOUR_ACCESS_KEY"; // your access key here.
        const long PhoneNumber = 31612345678; // your phone number here.

        static void Main(string[] args)
        {
            Client client = Client.CreateDefault(YourAccessKey);

            try
            {
                VerifyOptionalArguments optionalArguments = new VerifyOptionalArguments();
                // optionalArguments.Originator = "MessageBird";
                // optionalArguments.TokenLength = 8;
                // optionalArguments.Timeout = 60;

                MessageBird.Objects.Verify verify = client.CreateVerify(PhoneNumber, optionalArguments);
                Console.WriteLine("{0}", verify);

            }
            catch (ErrorException e)
            {
                // Either the request fails with error descriptions from the endpoint.
                if (e.HasErrors)
                {
                    foreach (Error error in e.Errors)
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
