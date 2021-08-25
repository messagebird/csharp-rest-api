using System;

namespace MessageBird.Exceptions
{
    public class RequestValidationException : Exception
    {
        public RequestValidationException(string message) : base(message) { }
        public RequestValidationException(string message, Exception inner) : base(message, inner) { }
    }
}
