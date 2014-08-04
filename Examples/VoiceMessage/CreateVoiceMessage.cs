using System;

using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Objects;

namespace Examples
{
    class CreateVoiceMessage
    {
        static void Main(string[] args)
        {
            Client client = Client.CreateDefault("YOUR_ACCESS_KEY");

            try
            {
                VoiceMessageOptionalArguments optionalArguments = new VoiceMessageOptionalArguments()
                {
                    Language = Language.English,
                    Voice = Voice.Female,
                    IfMachine = IfMachineOptions.Continue

                };
                VoiceMessage voiceMessage = client.SendVoiceMessage("This is a test message. The message is converted to speech and the recipient is called on his mobile.", new long[] { 31612345678 }, optionalArguments);
                Console.WriteLine("{0}", voiceMessage);
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
