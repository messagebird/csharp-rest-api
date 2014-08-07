using System;
using System.Collections.Generic;

using MessageBird.Objects;
using Newtonsoft.Json;

namespace MessageBird.Exceptions
{
    public class ErrorException : Exception
    {
        private readonly ICollection<Error> errors;

        // IEnumerable to be immitable.
        // TODO: should these really be based on the JSON Errors class, and not a resource translation of it?
        public IEnumerable<Error> Errors
        {
            get { return errors; }
        }

        public string Reason { get; private set; }

        public bool HasErrors
        {
            get { return (Errors != null) && (errors.Count > 0); }
        }

        public bool HasReason
        {
            get { return !String.IsNullOrEmpty(Reason); }
        }

        public ErrorException(string reason, Exception innerException = null)
            : base(reason, innerException)
        {
            Reason = reason;
        }

        public ErrorException(ICollection<Error> errors, Exception innerException)
            : base("multiple errors", innerException)
        {
            this.errors = errors;
        }

        // XXX: Solve explicit use of json deserialation, needs to be more generic!
        public static ErrorException FromResponse(string response, Exception innerException)
        {
            try
            {
                var errors = JsonConvert.DeserializeObject<Dictionary<string, List<Error>>>(response);
                return new ErrorException(errors["errors"], innerException);
            }
            catch (JsonSerializationException)
            {
                return null;
            }
        }
    }
}