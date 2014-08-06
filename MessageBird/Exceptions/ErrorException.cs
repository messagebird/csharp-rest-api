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
            get { return Errors != null && Errors.Count > 0; }
        }

        public bool HasReason
        {
            get { return !String.IsNullOrEmpty(Reason); }
        }

        public ErrorException(string reason, Exception innerException)
            : base(reason, innerException)
        {
            Reason = reason;
        }

        // TODO: refactor from `List` to `IEnumerable`
        public ErrorException(List<Error> errors, Exception innerException)
            : base("multiple errors", innerException)
        {
            Errors = errors;
        }

        // XXX: Solve explicit use of json deserialation, needs to be more generic!
        public static ErrorException FromResponse(string response, Exception innerException)
        {
            try
            {
                Dictionary<string, List<Error>> errors = JsonConvert.DeserializeObject<Dictionary<string, List<Error>>>(response);
                return new ErrorException(errors["errors"], innerException);
            }
            catch (JsonSerializationException)
            {
                return null;
            }
        }
    }
}
