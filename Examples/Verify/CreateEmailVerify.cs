using System;
using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Objects;

namespace Examples.Verify
{
    internal static class CreateEmailVerify
    {
        private const string YourAccessKey = "YOUR_ACCESS_KEY"; // your access key here.
        private const string RecipientEmail = "example@messagebird.com"; // your email address here.

        private static void Main(string[] args)
        {
            var client = Client.CreateDefault(YourAccessKey);

            try
            {
                var optionalArguments = new VerifyOptionalArguments
                {
                    Type = MessageType.Email,
                    Originator = "verify@yourdomain.com"
                };

                var verify = client.CreateVerify(RecipientEmail, optionalArguments);
                Console.WriteLine("{0}", verify);

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
