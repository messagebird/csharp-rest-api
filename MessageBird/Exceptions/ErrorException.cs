using System;
using System.Collections.Generic;

using MessageBird.Objects;
using Newtonsoft.Json;

namespace MessageBird.Exceptions
{
    public class ErrorException : Exception
    {
        public List<Error> Errors { get; private set; }

        public string Reason { get; private set; }

        public bool HasErrors
        {
            get {return Errors != null && Errors.Count > 0;}
        }

        public bool HasReason
        {
            get { return !String.IsNullOrEmpty(Reason); }
        }

        public ErrorException(string reason)
        {
            Reason = reason;
        }

        public ErrorException(List<Error> errors)
        {
            Errors = errors;
        }

       // XXX: Solve explicit use of json deserialation, needs to be more generic!
        public static ErrorException FromResponse(string response)
        {
            try
            {
                Dictionary<string, List<Error>> errors = JsonConvert.DeserializeObject<Dictionary<string, List<Error>>>(response);
                return new ErrorException(errors["errors"]);
            }
            catch (JsonSerializationException)
            {
                return null;
            }
        }
    }
}
